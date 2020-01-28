using System;
using McMaster.Extensions.CommandLineUtils;

namespace RandStr
{
    class Program
    {
        static int Main(string[] args)
        {
            var rootApp = new CommandLineApplication()
            {
                Name = "Random string"
            };

            rootApp.Command("random", app => 
            {
                var strArgs = app.Argument("random string", "");
                var _ln = app.Option("--length", "length of rand str", CommandOptionType.SingleOrNoValue);
                var _ltr = app.Option("--letter", "include letter/ not", CommandOptionType.SingleOrNoValue);
                var _num = app.Option("--number", "include number/ not", CommandOptionType.SingleOrNoValue);
                var _upr = app.Option("--uppercase", "uppercase letter/ not", CommandOptionType.SingleOrNoValue);
                var _lwr = app.Option("--lowercase", "lowercase letter/ not", CommandOptionType.SingleOrNoValue);

                int ln = 32;
                bool ltr = true;
                bool num = true;
                bool upr = false;
                bool lwr = false;
                
                app.OnExecute(()=>
                {
                    if(_ln.HasValue())
                        ln = Convert.ToInt32(_ln.Value());


                    if(_ltr.HasValue())
                        ltr = Convert.ToBoolean(_ltr.Value());
                    
                    if(_num.HasValue())
                        num = Convert.ToBoolean(_num.Value());

                    
                    if(_upr.HasValue())
                        upr = Convert.ToBoolean(_upr.Value());

                    
                    if(_lwr.HasValue())
                        lwr = Convert.ToBoolean(_lwr.Value());

                    Console.WriteLine(rand(ln, ltr, num, upr, lwr));
                });
            });

            return rootApp.Execute(args);
        }

        static string rand(int ln, bool ltr, bool num, bool uppercase, bool lowercase)
        {
            var random = new Random();
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] tempChars = new char[ln];

            if(!ltr)
            {
                for (int i = 0; i < ln; i++)
                    tempChars[i] = chars[random.Next(chars.Length-10, chars.Length)];

                return new String(tempChars);
            }

            if(!num)
            {
                for (int i = 0; i < ln; i++)
                    tempChars[i] = chars[random.Next(chars.Length-10)];

                if(uppercase)
                    return new String(tempChars).ToUpper();
                
                if(lowercase)
                    return new String(tempChars).ToLower();

                return new String(tempChars);
            }


            for (int i = 0; i < ln; i++)
                tempChars[i] = chars[random.Next(chars.Length)];


            if(uppercase)
                return new String(tempChars).ToUpper();
            
            if(lowercase)
                return new String(tempChars).ToLower();

            return new String(tempChars);
            
        }
    }
}
