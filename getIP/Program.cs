using System;
using System.Net;
using McMaster.Extensions.CommandLineUtils;

namespace getIP
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
                    string hostName = Dns.GetHostName();
                    var hostAddr = Dns.GetHostEntry(hostName);
                    foreach (var ip in hostAddr.AddressList)
                    {
                        if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            Console.WriteLine(ip.ToString());
                    }
                });
            });

            return rootApp.Execute(args);
        }
    }
}
