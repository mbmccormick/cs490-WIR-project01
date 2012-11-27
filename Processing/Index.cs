using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BillboardAnalyzer.Processing
{
    public class Index
    {
        public Dictionary<int, string> documents;
        public Dictionary<int, string> terms;
        
        public Index(string[] paths)
        {
            documents = new Dictionary<int, string>();
            terms = new Dictionary<int, string>();

            int termCount = 0;

            foreach (string document in paths)
            {
                StreamReader sr = new StreamReader(document);

                int key = 0;
                try
                {
                    key = Convert.ToInt32(Path.GetFileNameWithoutExtension(document));
                }
                catch (Exception ex)
                {
                    sr.Dispose();
                    continue;
                }

                string value = sr.ReadToEnd();

                documents.Add(key, value);

                foreach (string term in value.Split(' '))
                {
                    if (terms.ContainsValue(term) == false)
                    {
                        terms.Add(termCount, term);
                        termCount++;
                    }
                }

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

        public int GetNumberOfDocumentsContaining(int termId)
        {
            string term = terms[termId];

            int count = 0;
            foreach (var document in documents)
                if (document.Value.Contains(term) == true)
                    count++;

            return count;
        }
    }
}