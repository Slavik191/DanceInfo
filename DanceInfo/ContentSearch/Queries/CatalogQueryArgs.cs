using System;
using System.Collections.Generic;

namespace DanceInfo.ContentSearch.Queries
{
    public class CatalogQueryArgs
    {
        public CatalogQueryArgs()
        {
            Page = 1;
            Size = 5;
        }

        public string Query { get; set; }
        public List<String> Category { get; set; }
        public List<String> Tag { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }

    }
}