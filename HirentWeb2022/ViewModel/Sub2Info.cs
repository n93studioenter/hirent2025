using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HirentWeb2022.ViewModel
{
    public class Sub2Info
    {
        public long SubCate2ID { get; set; }
        public string SubCate2Name { get; set; }
        public Nullable<long> SubCate1ID { get; set; }
        public Nullable<bool> IsDisplay { get; set; }
        public string SubCate2Desc { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<int> Sort { get; set; }
        public string Language { get; set; }
        public string SubCate1Name { get; set; }
        public string MainCateName { get; set; }
        public long MainCateID { get; set; }
    }
}