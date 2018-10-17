using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace OpenJiraTerminal
{
    class CSVReader
    {
        public List<string[]> ReadAddUserCsv(string path)
        {
            List<string[]> table = new List<string[]>();
            StreamReader reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split("|");
                table.Add(values);
            }
            reader.Dispose();
            return table;
        }
    }
}
