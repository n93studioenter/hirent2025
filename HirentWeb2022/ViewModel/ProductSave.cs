using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HirentWeb2022.Models;
namespace HirentWeb2022.ViewModel
{
    public class ProductSave
    {
        public tb_Product tb_Product { get; set; }
        public tb_ProductTermConditionDetails tb_ProductTermConditionDetails { get;set;}
        public tb_ProductTermConditionDetails_Translation tb_ProductTermConditionDetails_Translation { get; set; }
    }
}