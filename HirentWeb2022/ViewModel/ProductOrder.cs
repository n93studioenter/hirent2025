using HirentWeb2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HirentWeb2022.ViewModel
{
    public class ProductOrder
    {
        public tb_Customer tb_Customer;
        public tb_Product tb_Product { get; set; }  
        public tb_Pre_Order tb_Pre_Order { get; set; } 
        public tb_Pre_Order_Details tb_Pre_Order_Details { get; set; }
        public tb_CustomerDeliveryAddress tb_CustomerDeliveryAddress { get; set; }  
        public List<tb_Pre_Order_accompanying> tb_Pre_Order_Accompanying { get; set; } = new List<tb_Pre_Order_accompanying>();
    }
}