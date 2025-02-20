using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HirentWeb2022.Models;
using Newtonsoft.Json;
using HirentWeb2022.ViewModel;
namespace HirentWeb2022.Areas.Admin.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: Admin/ProductCategory

        #region tb_CategoryMain
        public ActionResult Category_main()
        {
            using (var db=new HirentEntities())
            {
                var model = db.tb_CategoryMain.OrderBy(m=>m.Sort).ToList();
                return View(model);
            }
               
        }
        public JsonResult GetValue(int MainCateID)
        {
            using (var db = new HirentEntities())
            {
                var model = db.tb_CategoryMain.Where(m => m.MainCateID == MainCateID).FirstOrDefault();
                return Json(model, JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        public void SaveData(string data)
        {
            using (var db = new HirentEntities())
            {
                tb_CategoryMain model = JsonConvert.DeserializeObject<tb_CategoryMain>(data);
                tb_CategoryMain item;
                bool isAdd = false;
                if (model.MainCateID == 0)
                    isAdd = true;
                if (isAdd == true)
                {
                    item = model;
                    model.IsDeleted = false;
                    model.IsDisplay = true;
                    model.CreateDate = DateTime.Now;
                    db.tb_CategoryMain.Add(item);
                    db.SaveChanges();
                }
                else
                {
                    item = db.tb_CategoryMain.Where(m => m.MainCateID == model.MainCateID).FirstOrDefault();
                    item.MainCateName = model.MainCateName;
                    item.Language = model.Language;
                    item.MainCateDesc = model.MainCateDesc;
                    item.MainCateIcon = model.MainCateIcon;
                    item.Sort = model.Sort;
                    db.SaveChanges();
                }
            }
              
        }

        //

        #endregion

        #region tb_CategorySub1
        //tb_CategorySub1 
        public ActionResult CategorySub1()
        {
            using (var db = new HirentEntities())
            {
                ViewBag.ListCatemain = db.tb_CategoryMain.OrderBy(m => m.Sort).ToList();
                return View();
            } 
        }
        public ActionResult loadCateSub1(int MainCateID)
        {
            using (var db = new HirentEntities())
            {
               // var model = db.tb_CategorySub1.Where(m => m.MainCateID == MainCateID || MainCateID==0).OrderBy(m=>m.Sort).ToList();
               var model=(from c1 in db.tb_CategorySub1
                          join m in db.tb_CategoryMain
                          on c1.MainCateID equals m.MainCateID
                          select new Sub1Info()
                          {
                              CreateDate=c1.CreateDate,
                              IsDeleted=c1.IsDeleted,
                              IsDisplay=c1.IsDisplay,
                              Language=c1.Language,
                              MainCateID=c1.MainCateID,
                              Sort=c1.Sort,
                              SubCate1Desc=c1.SubCate1Desc,
                              SubCate1ID=c1.SubCate1ID,
                              SubCate1Name=c1.SubCate1Name,
                              MainCateName=m.MainCateName
                          }
                          ).Where(m => m.MainCateID == MainCateID || MainCateID == 0).OrderBy(m => m.Sort).ToList();
                return PartialView(model);
            }
        }
        [HttpPost]
        public void SaveDatasub1(string data)
        {
            using (var db = new HirentEntities())
            {
                tb_CategorySub1 model = JsonConvert.DeserializeObject<tb_CategorySub1>(data);
                tb_CategorySub1 item;
                bool isAdd = false;
                if (model.SubCate1ID == 0)
                    isAdd = true;
                if (isAdd == true)
                {
                    item = model;
                    model.IsDeleted = false;
                    model.IsDisplay = true;
                    model.CreateDate = DateTime.Now;
                    db.tb_CategorySub1.Add(item);
                    db.SaveChanges();
                }
                else
                {
                    item = db.tb_CategorySub1.Where(m => m.SubCate1ID == model.SubCate1ID).FirstOrDefault();
                    item.SubCate1Name = model.SubCate1Name;
                    item.Language = model.Language;
                    item.SubCate1Desc = model.SubCate1Desc;
                    item.MainCateID = model.MainCateID;
                    item.Sort = model.Sort;
                    db.SaveChanges();
                }
            }

        }
        public JsonResult GetValuesub1(int SubCate1ID)
        {
            using (var db = new HirentEntities())
            {
                var model = db.tb_CategorySub1.Where(m => m.SubCate1ID == SubCate1ID).FirstOrDefault();
                return Json(model, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion
        //tb_CategorySub2
        #region tb_CategorySub2

        public ActionResult CategorySub2()
        {
            using (var db = new HirentEntities())
            {
                ViewBag.ListCatemain = db.tb_CategoryMain.OrderBy(m => m.Sort).ToList();
                return View();
            } 
        }

        public ActionResult loadCateSub2( int SubCate1ID)
        {
            using (var db = new HirentEntities())
            {
                // var model = db.tb_CategorySub1.Where(m => m.MainCateID == MainCateID || MainCateID==0).OrderBy(m=>m.Sort).ToList();
                var model = (from c2 in db.tb_CategorySub2
                             join c1 in db.tb_CategorySub1
                             on c2.SubCate1ID equals c1.SubCate1ID
                             join m in db.tb_CategoryMain
                             on c1.MainCateID equals m.MainCateID
                             select new Sub2Info()
                             {
                                 CreateDate = c2.CreateDate,
                                 IsDeleted = c2.IsDeleted,
                                 IsDisplay = c2.IsDisplay,
                                 Language = c2.Language,
                                 SubCate2ID = c2.SubCate2ID,
                                 Sort = c2.Sort,
                                 SubCate2Desc = c2.SubCate2Desc,
                                 SubCate1ID = c1.SubCate1ID,
                                 SubCate2Name=c2.SubCate2Name,
                                 SubCate1Name = c1.SubCate1Name,
                                 MainCateName = m.MainCateName
                             }
                           ).Where(m => m.SubCate1ID == SubCate1ID || SubCate1ID == 0).OrderByDescending(m => m.Sort).ToList();
                return PartialView(model);
            }
        }

        public ActionResult loadselectcate1(int MainCateID)
        {
            using (var db = new HirentEntities())
            {
                var model = db.tb_CategorySub1.Where(m => m.MainCateID == MainCateID || MainCateID==0).ToList();
                return PartialView(model);
            }
        }

        [HttpPost]
        public void SaveDatasub2(string data)
        {
            using (var db = new HirentEntities())
            {
                tb_CategorySub2 model = JsonConvert.DeserializeObject<tb_CategorySub2>(data);
                tb_CategorySub2 item;
                bool isAdd = false;
                if (model.SubCate2ID == 0)
                    isAdd = true;
                if (isAdd == true)
                {
                    item = model;
                    model.IsDeleted = false;
                    model.IsDisplay = true;
                    model.CreateDate = DateTime.Now;
                    db.tb_CategorySub2.Add(item);
                    db.SaveChanges();
                }
                else
                {
                    item = db.tb_CategorySub2.Where(m => m.SubCate1ID == model.SubCate1ID).FirstOrDefault();
                    item.SubCate2Name = model.SubCate2Name;
                    item.Language = model.Language;
                    item.SubCate2Desc = model.SubCate2Desc;
                    item.SubCate1ID = model.SubCate1ID;
                    item.Sort = model.Sort;
                    db.SaveChanges();
                }
            }

        }

        public JsonResult GetValuesub2(int SubCate2ID)
        {
            using (var db = new HirentEntities())
            {
                //var model = db.tb_CategorySub2.Where(m => m.SubCate2ID == SubCate2ID).FirstOrDefault();

                var model = (from c2 in db.tb_CategorySub2
                             join c1 in db.tb_CategorySub1
                             on c2.SubCate1ID equals c1.SubCate1ID
                             join m in db.tb_CategoryMain
                             on c1.MainCateID equals m.MainCateID
                             select new Sub2Info()
                             {
                                 CreateDate = c2.CreateDate,
                                 IsDeleted = c2.IsDeleted,
                                 IsDisplay = c2.IsDisplay,
                                 Language = c2.Language,
                                 SubCate2ID = c2.SubCate2ID,
                                 Sort = c2.Sort,
                                 SubCate2Desc = c2.SubCate2Desc,
                                 SubCate1ID = c1.SubCate1ID,
                                 SubCate2Name = c2.SubCate2Name,
                                 SubCate1Name = c1.SubCate1Name,
                                 MainCateName = m.MainCateName,
                                 MainCateID=m.MainCateID
                             }
                          ).Where(m => m.SubCate2ID == SubCate2ID).OrderByDescending(m => m.Sort).FirstOrDefault();
                return Json(model, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

    }
}