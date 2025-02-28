using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HirentWeb2022.Models;
using HirentWeb2022.ViewModel;
namespace HirentWeb2022.Controllers
{
    public class WasehouseController : Controller
    {
        // GET: Wasehouse
        public ActionResult Index(int whid,int ? MainCateID)
        {
            if (!MainCateID.HasValue)
                MainCateID = 0;
            var getlang = Session["Lang"];
            if (getlang == null)
                getlang = "vi";
            ViewBag.lang = getlang;
            HirentEntities db = new HirentEntities();
            tb_WareHouse tb_WareHouse = db.tb_WareHouse.Where(m => m.whId == whid).FirstOrDefault();
            ViewBag.tb_WareHouse = tb_WareHouse;
            var model = (from p in db.tb_Product.ToList()
                         join ps in db.tb_ProductCategorySelection.ToList()
                         on p.ProductID equals ps.ProductId.Value
                         join pe in db.tb_Product_Translation
                         on p.ProductID equals pe.Idproduct.Value
                         join td in db.tb_ProductTermConditionDetails
                         on p.ProductID equals td.ProductId.Value
                         join wh in db.tb_WareHouse
                         on td.WarehouseId equals wh.whId
                         where td.WarehouseId.Value==whid
                         && (MainCateID==0 || ps.ProductMainCate.Value== MainCateID)
                         select new ProductVM()
                         {
                             tb_Product = p,
                             tb_Product_Translation = pe,
                             tb_ProductCategorySelection = ps,
                             tb_WareHouse = wh
                         }
                               ).OrderByDescending(m => m.tb_Product.ProductID).ToList();
            ViewBag.ProductTotal = model.Count;
            List<tb_CategoryMain> lsttb_CategoryMain = new List<tb_CategoryMain>();
            foreach (var item in model)
            {
                tb_ProductCategorySelection tb_ProductCategorySelection = db.tb_ProductCategorySelection.Where(m => m.ProductId == item.tb_Product.ProductID).FirstOrDefault();
                if (tb_ProductCategorySelection != null)
                {
                    tb_CategoryMain tb_CategoryMain = db.tb_CategoryMain.Find(tb_ProductCategorySelection.ProductMainCate.Value);
                    if (!lsttb_CategoryMain.Contains(tb_CategoryMain))
                        lsttb_CategoryMain.Add(tb_CategoryMain);
                }
            }
            ViewBag.Listcate = lsttb_CategoryMain;
            return View(model);
        }
    }
}