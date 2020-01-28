using System;
using System.Threading.Tasks;
using System.Threading;
using McMaster.Extensions.CommandLineUtils;
using SSObj;

namespace screenshot1
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var rootApp = new CommandLineApplication(){Name="screenshot1"};
            rootApp.Command("screenshot", app=>
            {
                var urlArgs = app.Argument("urlArgument", "the url");
                var formatOpt = app.Option("--format", "option for the output format", CommandOptionType.SingleOrNoValue);
                var nameOpt = app.Option("--output", "define the output file name", CommandOptionType.SingleOrNoValue);
                object format = null;

                app.OnExecuteAsync(async cancellationToken =>
                {
                    Console.WriteLine("executing...");
                    if(formatOpt.HasValue())
                        format = SS.detectFormatByOpt(formatOpt);

                    else if(nameOpt.HasValue())
                        format = SS.detectFormatByName(nameOpt);

                    else
                        format = SS.getDefaultFormat();
                    
                    Console.WriteLine("format : {0}", format);
                    
                    string name = SS.GetName(nameOpt);
                    await SS.TakeSS(urlArgs.Value, format, name);

                });
            });

            return rootApp.Execute(args);
        }
    }
}
