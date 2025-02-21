using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HirentWeb2022.Models;
namespace HirentWeb2022.Areas.Admin.Controllers
{
    public class SlideShowController : Controller
    {
        // GET: Admin/SlideShow
        public ActionResult Index()
        {
            var db = new HirentEntities();
            var model = db.tb_HomeMainBanner.ToList();
            return View(model);
        }

        [HttpPost]
        public bool SaveData(tb_HomeMainBanner tb_HomeMainBanner)
        {
            try
            {
                var db = new HirentEntities();
                tb_HomeMainBanner.BannerUpload = DateTime.Now;
                tb_HomeMainBanner.SortArr = 1;
                db.tb_HomeMainBanner.AddOrUpdate(tb_HomeMainBanner); 
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public JsonResult GetData(int HomeMainBannerID)
        {

            var db = new HirentEntities();
            var account = db.tb_HomeMainBanner.Find(HomeMainBannerID);

            return Json(account, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool DeleteDat   (int HomeMainBannerID)
        {
            try
            {
                var db = new HirentEntities();
                var account = db.tb_HomeMainBanner.Find(HomeMainBannerID);
                if (account != null)
                {
                    db.tb_HomeMainBanner.Remove(account);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}