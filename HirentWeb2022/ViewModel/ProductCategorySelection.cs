using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HirentWeb2022.ViewModel
{
    public class ProductCategorySelection
    {
        public int ProductId { get; set; }
        public int ProductMainCateID { get; set; }
        public string ProductMainCateName { get; set; }
        public int ProductSubCate1ID { get; set; }
        public string  ProductSubCate1Name { get; set; } 
        public int ProductSubCate2ID { get; set; }
        public string ProductSubCate2Name { get; set; }
    }
}