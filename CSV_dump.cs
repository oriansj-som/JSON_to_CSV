using System.Collections.Generic;
using System.IO;

namespace Json_to_CSV
{
    static public class CSV_dump
    {
        static public void dump(string filename, List<string> headers, List<List<string>> fields)
        {
            StreamWriter w = new StreamWriter(filename);
            w.Write(scrub("\"" + string.Join("\",\"", prescrub(headers)) + "\"\n"));
            foreach(List<string> l in fields)
            {
                w.Write(scrub("\"" + string.Join("\",\"", prescrub(l)) + "\"\n"));
            }
            w.Flush();
            w.Close();

        }

        static private List<string> prescrub(List<string> s)
        {
            List<string> r = new List<string>();
            foreach(string i in s)
            {
                string hold = i.Replace("\"", "\"\"");
                r.Add(hold);
            }

            return r;
        }
        static private string scrub(string s)
        {
            string hold = s.Replace(",\"\",", ",,");
            
            return hold;
        }

    }
}
