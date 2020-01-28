using System;
using McMaster.Extensions.CommandLineUtils;

namespace arit
{
    class Program
    {
        static int Main(string[] args)
        {
            var rootApp = new CommandLineApplication()
            {
                Name = "Aritmathics",
                Description = " This app is used for manipulate string; lowercase, uppercase, capitalize"
            };

            rootApp.Command("add", app =>
            {
                var vals = app.Argument("values", "value to operate", true);
                app.OnExecute(()=>
                {
                    int ret = 0;
                    foreach (var val in vals.Values)
                        ret += Convert.ToInt32(val);

                    Console.WriteLine(ret);
                });
            });

            rootApp.Command("substract", app =>
            {
                var vals = app.Argument("values", "value to operate", true);
                app.OnExecute(()=>
                {
                    int ret = Convert.ToInt16(vals.Values[0]);
                    for(int i=1; i< vals.Values.Count; i++)
                        ret -= Convert.ToInt16(vals.Values[i]);

                    Console.WriteLine(ret);
                });
            });

            rootApp.Command("divide", app =>
            {
                var vals = app.Argument("values", "value to operate", true);
                app.OnExecute(()=>
                {
                    int ret = Convert.ToInt16(vals.Values[0]);
                    for(int i=1; i< vals.Values.Count; i++)
                        ret /= Convert.ToInt16(vals.Values[i]);

                    Console.WriteLine(ret);
                });
            });

            return rootApp.Execute(args);
        }
    }
}
