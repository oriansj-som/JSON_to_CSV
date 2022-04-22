using System.Collections.Generic;
using System.Linq;

namespace Json_to_CSV
{
    static public class common
    {
        static public List<string> string_to_list(string s)
        {
            string[] array = s.Split(':');
            return array.ToList();
        }

        static public string string_to_set_Title(string s)
        {
            string[] array = s.Split("::");
            return array[0];
        }

        static public string string_to_set_Fetcher(string s)
        {
            string[] array = s.Split("::");
            return array[1];
        }
    }
}
