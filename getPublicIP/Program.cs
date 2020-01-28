using System;
using IPGetterObj;
using McMaster.Extensions.CommandLineUtils;

namespace getPublicIP
{
    class Program
    {
        static int Main(string[] args)
        {
            var rootApp = new CommandLineApplication(){
                Name = "IP getter"
            };

            rootApp.Command("ip", app=>
            {
                app.OnExecute(()=>
                {
                    IPGetter ipget = new IPGetter("https://ifconfig.me");
                    Console.WriteLine($"your public ip : {ipget.GetPublicIP()}");
                });
            });

            return rootApp.Execute(args);
        }
    }
}
