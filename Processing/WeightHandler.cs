using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillboardAnalyzer.Processing
{
    public class WeightHandler
    {
        Index index;

        public static void Process()
        {

        }

        private double ComputeWeight(int documentId, int termId, int documentTermFrequency, double queryTermWeight)
        {
            //int length = index.GetDocumentLength(documentId);
            //int averageLength = index.GetAverageDocumentLength();
            //int total = index.GetDocumentCount();
            //int contains = index.GetNumberOfDocumentsContaining(termId);

            //return (docTermFreq / (docTermFreq + 0.5 + (1.5 * len / avgDocLen))) * log(((double)totalNumDocs - ((double)numDocsContain + 0.5)) / ((double)numDocsContain + 0.5)) * ((8 + qryTermWeight) / (7 + qryTermWeight));

            return 0.0;
        }
    }
}