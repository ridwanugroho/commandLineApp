using System;
using System.Text;
using McMaster.Extensions.CommandLineUtils;

namespace obfuscator
{
    class Program
    {
        static int Main(string[] args)
        {
            var rootApp = new CommandLineApplication()
            {
                Name = "obfuscator",
                Description = "to obfusing text"
            };

            rootApp.Command("obfuscate", app => 
            {
                var strArgs = app.Argument("obfuscator", "");
                app.OnExecute(()=>
                {
                    Console.WriteLine(obfuse(strArgs.Value));
                });
            });

            return rootApp.Execute(args);

        }

        static string obfuse(string str)
        {
            string ret = "";
            var charInAscii = Encoding.ASCII.GetBytes(str);
            foreach (var c in charInAscii)
                ret += $"&#{Convert.ToString(c)};";

            return ret;
        }
    }
}
