using System;
using System.Linq;
using McMaster.Extensions.CommandLineUtils;

namespace palindrome
{
    class Program
    {
        static int Main(string[] args)
        {
            var rootApp = new CommandLineApplication()
            {
                Name = "Palindrome detector",
                Description = "detect palindrome words, phrases, and sentences"
            };

            var strArgs = rootApp.Argument("str", "string to be checked");
            rootApp.OnExecute(()=>
            {
                Console.WriteLine(palindrome(strArgs.Value));
            });

            return rootApp.Execute(args);

        }

        static bool palindrome(string str)
        {
            var strCharOnly = new string(str.Where(Char.IsLetter).ToArray());
            strCharOnly = strCharOnly.ToLower();
            int strLn = strCharOnly.Length;
            for(int i=0; i<strLn/2; i++)
            {
                if(strCharOnly[i] != strCharOnly[strLn-1-i])
                    return false;
            }

            return true;
        }
    }
}
