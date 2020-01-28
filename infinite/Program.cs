using System;
using McMaster.Extensions.CommandLineUtils;

namespace infinite
{
    class Program
    {
        static int Main(string[] args)
        {
            var rootApp = new CommandLineApplication(){Name="infinite sum"};
            rootApp.Command("sum", app=>
            {
                app.OnExecute(()=>
                {
                    int ret = 0;
                    int i=1;
                    while(ret != null)
                    {
                        string temp = Prompt.GetString($"masukkan angka ke-{i} : ");
                        if(temp != null)
                            ret += Convert.ToInt32(temp);

                        else
                            break;

                        i++;
                    }

                    Console.WriteLine(ret);
                    
                });
            });

            return rootApp.Execute(args);
        }
    }
}
