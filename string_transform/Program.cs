using System;
using System.Globalization;
using McMaster.Extensions.CommandLineUtils;

namespace string_transform
{
    class Program
    {
        static int Main(string[] args)
        {
            var rootApp = new CommandLineApplication()
            {
                Name = "String Manipulaor",
                Description = " This app is used for manipulate string; lowercase, uppercase, capitalize"
            };

            rootApp.Command("lower", app =>
            {
                var text = app.Argument("text", "input text");
                app.OnExecute(()=>
                {
                    Console.WriteLine(text.Value.ToLower());
                });
            });

            rootApp.Command("upper", app =>
            {
                var text = app.Argument("text", "input text");
                app.OnExecute(()=>
                {
                    Console.WriteLine(text.Value.ToLower());
                });
            });

            rootApp.Command("capital", app =>
            {
                TextInfo ti = new CultureInfo("en-US", false).TextInfo;
                var text = app.Argument("text", "input text");
                app.OnExecute(()=>
                {
                    Console.WriteLine(ti.ToTitleCase(text.Value));
                });
            });

            return rootApp.Execute(args);
        }
    }
}
