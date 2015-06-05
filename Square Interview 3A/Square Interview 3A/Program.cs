using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Square_Interview_3A
{
    class DataBase
    {
        private Dictionary<string, Dictionary<string, HashSet<string>>> indicies;

        private Dictionary<string, Dictionary<string, string>> table;

        public DataBase()
        {
            indicies = new Dictionary<string, Dictionary<string, HashSet<string>>>();
            table = new Dictionary<string, Dictionary<string, string>>();
        }

        public void Put(string key, Dictionary<string, string> row)
        {
            if (table.ContainsKey(key))
            {
                Remove(key);
            }

            table.Add(key, row);

            foreach (KeyValuePair<string, Dictionary<string, HashSet<string>>> indexWithColumn in indicies)
            {
                string columnName = indexWithColumn.Key;
                Dictionary<string, HashSet<string>> index = indexWithColumn.Value;
                if (row.ContainsKey(columnName))
                {
                    string value = row[columnName];
                    if (index.ContainsKey(value))
                    {
                        index[value].Add(key);
                    }
                    else
                    {
                        HashSet<string> set = new HashSet<string>();
                        set.Add(key);
                        index.Add(value, set);
                    }
                }
            }
        }

        public Dictionary<string, string> Get(string key)
        {
            return table[key];
        }

        public void Remove(string key)
        {
            Dictionary<string, string> row = table[key];
            
            table.Remove(key);

            foreach (var columnValuePair in row)
            {
                if (indicies.ContainsKey(columnValuePair.Key))
                {
                    Dictionary<string, HashSet<string>> index = indicies[columnValuePair.Key];
                    HashSet<string> keys = index[columnValuePair.Value];
                    keys.Remove(key);
                }
            }
        }

        public void CreateIndex(string column)
        {
            Dictionary<string, HashSet<string>> index = new Dictionary<string, HashSet<string>>();
            foreach (KeyValuePair<string, Dictionary<string, string>> keyAndRow in table)
            {
                if (keyAndRow.Value.ContainsKey(column))
                {
                    string value = keyAndRow.Value[column];
                    if (index.ContainsKey(value))
                    {
                        index[value].Add(keyAndRow.Key);
                    }
                    else
                    {
                        HashSet<string> set = new HashSet<string>();
                        set.Add(keyAndRow.Key);
                        index.Add(value, set);
                    }
                }
                
            }
            indicies.Add(column, index);
        }

        public HashSet<string> FetchIndex(string column, string value)
        {
            return indicies[column][value];
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            RunTests();
        }

        static void RunTests()
        {
            DataBase db = new DataBase();
            db.Put("1", new Dictionary<string, string>() { { "name", "John" }, { "age", "10" } });
            db.Put("2", new Dictionary<string, string>() { { "name", "Paul" }, { "age", "10" } });
            db.Put("3", new Dictionary<string, string>() { { "name", "Mark" }, { "age", "14" } });

            db.CreateIndex("age");
            HashSet<string> set = db.FetchIndex("age", "10");

            db.Remove("1");
            set = db.FetchIndex("age", "10");

            db.Put("1", new Dictionary<string, string>() { { "name", "John" }, { "age", "10" } });
            set = db.FetchIndex("age", "10");

            db.Put("1", new Dictionary<string, string>() { { "name", "John" }, { "age", "11" } });

            set = db.FetchIndex("age", "11");
        }
    }
}
