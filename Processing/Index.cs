using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TuneRank.Processing
{
    public class Index
    {
        // define document and term collections
        public Dictionary<int, string> documents;
        public Dictionary<int, string> terms;
        
        public Index(string[] paths)
        {
            // initialize collections
            documents = new Dictionary<int, string>();
            terms = new Dictionary<int, string>();

            int termCount = 0;

            // loop through each document in the provided paths
            foreach (string document in paths)
            {
                // initialize the stream reader
                StreamReader sr = new StreamReader(document);

                int key = 0;
                try
                {
                    // extract te song ID from the filename
                    key = Convert.ToInt32(Path.GetFileNameWithoutExtension(document));
                }
                catch (Exception ex)
                {
                    sr.Dispose();
                    continue;
                }

                // read the document from disk
                string value = sr.ReadToEnd();

                // add the document to the collection
                documents.Add(key, value);

                // tokenize the document
                foreach (string term in value.Split(' '))
                {
                    // check if term exists in collection
                    if (terms.ContainsValue(term) == false)
                    {
                        // add term to collection
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