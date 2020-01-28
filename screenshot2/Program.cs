using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;
using SSObj;

namespace screenshot2
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var rootApp = new CommandLineApplication(){Name="screenshot2"};
            rootApp.Command("screenshot-list", app=>
            {
                var filePath = app.Argument("urlArgument", "the url");
                var formatOpt = app.Option("--format", "option for the output format", CommandOptionType.SingleOrNoValue);
                object format = null;

                app.OnExecuteAsync(async cancellationToken =>
                {
                    List<string> links = new List<string>(File.ReadAllLines(filePath.Value));

                    Console.WriteLine("executing...");
                    if(formatOpt.HasValue())
                        format = SS.detectFormatByOpt(formatOpt);

                    else
                        format = SS.getDefaultFormat();
                    
                    Console.WriteLine("format : {0}", format);
                    foreach(var l in links)
                    {
                        string name = SS.GetName(l);
                        Console.WriteLine(name);
                        await SS.TakeSS(l, format, name);
                    }
                });
            });

            return rootApp.Execute(args);
        }
    }
}
