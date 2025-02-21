using HirentWeb2022.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace HirentWeb2022.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult AccountManager()
        {
            var db = new HirentEntities();
            var model = db.tb_LocalAccount.Where(m=>m.PermissionID!=0).ToList();
            return View(model);
        }

        public JsonResult GetData(int LocalAccountID)
        {

            var db = new HirentEntities();
            var account = db.tb_LocalAccount.Find(LocalAccountID);

            return Json(account, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public bool SaveData(tb_LocalAccount tb_LocalAccount)
        {
            try
            {
                var db = new HirentEntities();
                tb_LocalAccount.PermissionID = 1;
                db.tb_LocalAccount.AddOrUpdate(tb_LocalAccount);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        public bool DeleteData(int LocalAccountID)
        {
            try
            {
                var db = new HirentEntities();
                var account = db.tb_LocalAccount.Find(LocalAccountID);
                if (account != null)
                {
                    db.tb_LocalAccount.Remove(account);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            ViewBag.PermissionID = Helper.GetPermission();
            return View();
        }
        public ActionResult UserInfo()
        {
            ViewBag.Name = Helper.GetUsername();
           return  PartialView();
        }
        public ActionResult MenuLeft()
        {
            ViewBag.PermissionID = Helper.GetPermission();
            return PartialView();   
        }
        public void Logout()
        {
            if (Request.Cookies["hirenadmin"] != null)
            {
                var cookie = new HttpCookie("hirenadmin")
                {
                    Expires = DateTime.Now.AddDays(-5) // Đặt ngày hết hạn trong quá khứ
                };
                Response.Cookies.Add(cookie);
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public int Login(string username,string password)
        {
            using(var db=new HirentEntities())
            {
                var checkLogin = db.tb_LocalAccount.Where(m => m.UserName == username && m.Password == password).FirstOrDefault();
                if(checkLogin != null)
                {
                    HttpCookie userinfo1 = new HttpCookie("hirenadmin");
                    userinfo1["LocalAccountID"] = checkLogin.LocalAccountID.ToString();
                    userinfo1["username"] = checkLogin.UserName;
                    userinfo1["PermissionID"] = checkLogin.PermissionID.Value.ToString();
                    userinfo1.Expires = DateTime.Now.AddDays(2);
                    Response.Cookies.Add(userinfo1);
                }
                return checkLogin!=null?1:0;
            }
        }
    }
}