using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Mono.Options;

namespace sharpCat
{
    internal class Program
    {
        static string repeatString(string str, int amount)
        {
            return string.Concat(Enumerable.Repeat(str, amount));
        }
        static void readFile(string filename, bool showBanner)
        {
            string[] text = File.ReadAllLines(filename);
            Console.WriteLine(showBanner ? $"CONTENTS OF {filename}\n{repeatString("-", filename.Length + 12)}" : "");
            foreach (string line in text)
            {
                Console.WriteLine(line);
            }
        }

        private static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: sharpCat.exe [OPTIONS]+ file");
            Console.WriteLine("Displays contents of a file or files to stdout\n");
            Console.WriteLine("Options: ");
            p.WriteOptionDescriptions(Console.Out);
            
        }
        public static bool IsInArray<T>(T[] array, T elem)
        {
            return array.Contains(elem);
        }
        static void Main(string[] args)
        {
            var shouldShowBanner      = false;
            var shouldShowLineNumber  = false;
            var shouldShowHelp        = false;
            var shouldShowLineEndings = false;
            var p = new OptionSet
            { 
                {"b|banner", "shows the name of the file",         b => shouldShowBanner      = true},//b != null},//--banner
                {"n|number", "shows the line number",              n => shouldShowLineNumber  = true},//n != null},//--number
                {"e|show-ends", "shows $ at the end of each line", e => shouldShowLineEndings = true},//e != null},//--show-ends
                {"h|help", "show this message and exit",           h => shouldShowHelp        = true},//h != null}
            };
            string[] commands = { "-b", "--banner", "-n", "--number", "-e", "--show-ends", "-h", "--help" };
            List<string> extra;
            try
            {
                extra = p.Parse(args);
            } catch (OptionException e)
            {
                Console.Write("sharpCat.exe: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `sharpCat.exe --help` for more");
                return;
            }

            if (shouldShowHelp)
            {
                ShowHelp(p);
                return;
            }
            foreach (var arg in args)
            {
                /* arg is not a file if its in commands */
                if (IsInArray(commands, arg)) {
                    //Console.WriteLine(arg); 
                    return;
                }
                if (!File.Exists(arg))
                {
                    Console.WriteLine("COULD NOT FIND FILE");
                    return;
                }
                readFile(arg, shouldShowBanner);
            }
        }
    }
}
