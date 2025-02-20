using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HirentWeb2022.ViewModel
{
    public class ProductCateSearch
    {
        public string Name { get; set; }        
        public string url { get; set; }

    }
    public class ProductSeacch
    {
        public string Name { get; set; }
        public string url { get; set; }
        public string Images { get; set; }

    }
    public class AutoCompleteSearch
    {
        public List<ProductCateSearch> productCateSearches { get; set; } = new List<ProductCateSearch>();
        public List<ProductSeacch> productSearches { get; set; }=new List<ProductSeacch>();

    }
}