using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Json_to_CSV
{
    class JSON_Parse
    {
        static public List<List<string>> all_records;
        static public void init(string filename, string master, List<string> fields)
        {
            all_records = new List<List<string>>();
            string json = string.Empty;
            using (StreamReader streamReader = new StreamReader(filename))
            {
                json = streamReader.ReadToEnd();
            }
            JObject oby = JObject.Parse(json);

            JToken toky = oby.SelectToken(master);
            IEnumerable<JToken> set = toky.Children();
            if(null == set)
            {
                Console.WriteLine("Master not found in dataset");
                Environment.Exit(2);
            }
            int count = set.Count();
            int iteration = 1;
            foreach (JToken i in set)
            {
                Console.WriteLine(string.Format("processing record {0} of {1}", iteration, count));
                JObject send = i.ToObject<JObject>();
                List<string> s = JSON_Parse.collect_record(send, fields);
                all_records.Add(s);
                iteration = iteration + 1;
            }
        }
        static private List<string> collect_record(JObject send, List<string> fields)
        {
            List<string> r = new List<string>();
            foreach (string s in fields)
            {
                string field = recursive_parse(send, common.string_to_list(s));
                r.Add(field);
            }
            return r;
        }
        static private string recursive_parse(JObject oby, List<string> dig)
        {
            string s = dig.First();
            dig.RemoveAt(0);
            JToken toky = oby.SelectToken(s);
            if(null == toky)
            {
                Console.WriteLine(string.Format("No such field as {0} in {1}", s, oby));
                return "";
            }
            if (toky.Count() == 1)
            {
                Console.WriteLine(string.Format("woops: {0}", s));
            }
            if (dig.Count > 0)
            {
                try
                {
                    JObject send = toky.ToObject<JObject>();
                    return recursive_parse(send, dig);
                }
                catch (Exception ex)
                {
                    JObject send = toky.Value<JObject>();
                    return recursive_parse(send, dig);
                }
            }
            return toky.ToString();

        }
    }
}
