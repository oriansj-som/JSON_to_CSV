using System;
using System.Collections.Generic;

namespace Json_to_CSV
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Json_to_CSV");
            string master = null;
            string input = null;
            string output = "output.csv";
            List<string> fields = new List<string>();
            List<string> headers = new List<string>();
            int i = 0;
            while (i < args.Length)
            {
                if (match("--field", args[i]))
                {
                    string f = args[i + 1];
                    fields.Add(common.string_to_set_Fetcher(f));
                    headers.Add(common.string_to_set_Title(f));
                    i = i + 2;
                }
                else if (match("--master", args[i]))
                {
                    master = args[i + 1];
                    i = i + 2;
                }
                else if (match("--input-file", args[i]))
                {
                    input = args[i + 1];
                    i = i + 2;
                }
                else if (match("--output-file", args[i]))
                {
                    output = args[i + 1];
                    i = i + 2;
                }
                else
                {
                    Console.WriteLine(string.Format("Unknown argument: {0} received", args[i]));
                    i = i + 1;
                }
            }

            if(null == master)
            {
                Console.WriteLine("You need to set thte master set with --master");
                Environment.Exit(1);
            }
            if (null == master)
            {
                Console.WriteLine("You need to set the input file with --input-file");
                Environment.Exit(1);
            }
            JSON_Parse.init(input, master , fields);
            CSV_dump.dump(output, headers, JSON_Parse.all_records);
            Console.WriteLine("Processing complete");
        }

        static bool match(string a, string b)
        {
            return a.Equals(b, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
