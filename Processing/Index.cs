using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BillboardAnalyzer.Processing
{
    public class Index
    {
        Dictionary<int, string> documents;
        Dictionary<int, string> terms;
        
        public Index(string[] paths)
        {
            documents = new Dictionary<int, string>();

            foreach (string path in paths)
            {
                StreamReader sr = new StreamReader(path);
                
                int key = Convert.ToInt32(Path.GetFileNameWithoutExtension(path));
                string value = sr.ReadToEnd();

                documents.Add(key, value);

                sr.Dispose();
            }
        }

        public int GetDocumentLength(int id)
        {
            return documents[id].Length;
        }

        public int GetAverageDocumentLength()
        {
            int length = 0;
            foreach (var document in documents)
                length = length + document.Value.Length;

            return length / documents.Count;
        }

        public int GetDocumentCount()
        {
            return documents.Count;
        }

        public int GetNumberOfDocumentsContaining(string query)
        {
            int count = 0;
            foreach (var document in documents)
                if (document.Value.Contains(query) == true)
                    count++;

            return count;
        }
    }
}