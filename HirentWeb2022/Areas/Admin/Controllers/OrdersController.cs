using DocumentFormat.OpenXml.EMMA;
using HirentWeb2022.Models;
using HirentWeb2022.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace HirentWeb2022.Areas.Admin.Controllers
{
    public class OrdersController : Controller
    {

        public void Xacnhandon(int pOrderId)
        {
            HirentEntities db = new HirentEntities();
            tb_Pre_Order tb_Pre_Order = db.tb_Pre_Order.Find(pOrderId);
            if (tb_Pre_Order != null)
            {
                tb_Pre_Order.status = 3;
                db.SaveChanges();
            }
        }
        // GET: Admin/OrderList
        public ActionResult OrderList()
        {
            HirentEntities db = new HirentEntities();
            List<ProductOrder> model = new List<ProductOrder>();
            var getlistordr = db.tb_Pre_Order.Where(m=>m.status!=1).OrderByDescending(m => m.pOrderId).ToList();
            foreach (var item in getlistordr)
            {
                ProductOrder ProductOrder = new ProductOrder();
                ProductOrder.tb_Pre_Order = item;
                ProductOrder.tb_Pre_Order_Details = db.tb_Pre_Order_Details.Where(m => m.pOrderId == item.pOrderId).FirstOrDefault();
                ProductOrder.tb_Product = db.tb_Product.Where(m => m.ProductID == item.productId).FirstOrDefault();
                ProductOrder.tb_Pre_Order_Accompanying = db.tb_Pre_Order_accompanying.Where(m => m.pOrderId == item.pOrderId).ToList();
                ProductOrder.tb_Customer = db.tb_Customer.Where(m => m.CustomerID == item.customerId).FirstOrDefault();
                model.Add(ProductOrder);
            }
            return View(model);
        }
        public ActionResult OrderDetail(int id) 
        {
            ViewBag.lang = "vi";
            HirentEntities db = new HirentEntities();
            List<ProductOrder> model = new List<ProductOrder>();
            var getlistordr = db.tb_Pre_Order.Where(m=>m.pOrderId==id ).OrderByDescending(m => m.pOrderId).ToList();
            foreach (var item in getlistordr)
            {
                ProductOrder ProductOrder = new ProductOrder();
                ProductOrder.tb_Pre_Order = item;
                ProductOrder.tb_Pre_Order_Details = db.tb_Pre_Order_Details.Where(m => m.pOrderId == item.pOrderId).FirstOrDefault();
                ProductOrder.tb_Product = db.tb_Product.Where(m => m.ProductID == item.productId).FirstOrDefault();
                ProductOrder.tb_Pre_Order_Accompanying = db.tb_Pre_Order_accompanying.Where(m => m.pOrderId == item.pOrderId).ToList();
                ProductOrder.tb_Customer = db.tb_Customer.Where(m => m.CustomerID == item.customerId).FirstOrDefault();
                ProductOrder.tb_CustomerDeliveryAddress = db.tb_CustomerDeliveryAddress.Where(m => m.CustomerID == ProductOrder.tb_Customer.CustomerID && m.IsMacdinh==1).FirstOrDefault();

                model.Add(ProductOrder);
            }
            return View(model);
        }
    }
}