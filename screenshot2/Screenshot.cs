using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using PuppeteerSharp;
using McMaster.Extensions.CommandLineUtils;
using PuppeteerSharp.Media;

namespace SSObj
{
    public class SS
    {
        public static object detectFormatByOpt(CommandOption opt)
        {
            Console.WriteLine("detecting format : {0}", opt.Value());
            object format = null;
            switch (opt.Value())
            {
                case "jpg":
                format = ScreenshotType.Jpeg;
                break;

                case "png":
                format = ScreenshotType.Png;
                break;

                case "pdf":
                format = new PdfOptions(){
                    Format = PaperFormat.A4,
                    Landscape = true
                };
                break;

                default:
                Console.WriteLine("incorrect format! only jgp, png, pdf!");
                return null;
            }

            return format;
        }

        public static ScreenshotType getDefaultFormat()
        {
            return ScreenshotType.Png;
        }

        private static List<string> getFileName(string[] files)
        {
            List<string> file = new List<string>();
            foreach(var f in files)
                file.Add(Path.GetFileNameWithoutExtension(f));
            
            return file;
        }

        private static int getSequence(List<string> files, string targetName)
        {
            int seq = 0;
            foreach(var f in files)
            {
                if(f.Contains(targetName))
                    seq++;
            }

            return seq;
        }

        public static string GetName(string name)
        {
            string nama = "";
            string candidate = name.Replace("/", "_").Replace(":", "_").Replace("?", "_").Replace("\"", "_")
                        .Replace("<", "_").Replace(">", "_").Replace("|", "_");
            var files = getFileName(Directory.GetFiles(Directory.GetCurrentDirectory(), 
                                                            ".", 
                                                            SearchOption.TopDirectoryOnly));
            if(files.Contains(candidate))
            {
                nama = candidate + "_" + Convert.ToString(getSequence(files, name)) + ".";
                Console.WriteLine("nama sudah ada");
            }

            else
            {
                nama = candidate + ".";
                Console.WriteLine("nama belum ada");
            }

            return nama;
        }

        public static async Task TakeSS(string url, object type, string name)
        {
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Args = new [] {
                                "--proxy-server=",
                                "--no-sandbox",
                                "--disable-infobars",
                                "--disable-setuid-sandbox",
                                "--ignore-certificate-errors",
                            },
            });

            try
            {
                using (var page = await browser.NewPageAsync())
                {
                    Console.WriteLine($"rendering {url}");
                    await page.GoToAsync(url);
                    Console.WriteLine($"screenshot in progress");
                    if(type is ScreenshotType)
                    {
                        ScreenshotOptions ssOpt = new ScreenshotOptions(){FullPage = true, Type = (ScreenshotType)type};
                        await page.ScreenshotAsync(name+(ScreenshotType)type, ssOpt);
                    }

                    else
                    {
                        Console.WriteLine("gengerating pdf");
                        
                        await page.PdfAsync(name + "pdf");
                    }

                    await page.WaitForTimeoutAsync(1000);
                }
            }

            catch (System.Exception e)
            {
                Console.WriteLine("gagal!");
                Console.WriteLine(e);
                await browser.CloseAsync();
            }

            await browser.CloseAsync();
        }
    }
}