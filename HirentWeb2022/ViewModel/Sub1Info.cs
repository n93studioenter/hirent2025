using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HirentWeb2022.ViewModel
{
    public class Sub1Info
    {
        public long SubCate1ID { get; set; }
        public string SubCate1Name { get; set; }
        public Nullable<long> MainCateID { get; set; }
        public Nullable<bool> IsDisplay { get; set; }
        public string SubCate1Desc { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<byte> Sort { get; set; }
        public string Language { get; set; }
        public string MainCateName { get; set; }
    }
}