using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using HirentWeb2022.Models;
using HirentWeb2022.ViewModel;
using Newtonsoft.Json;

namespace HirentWeb2022.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product
        public ActionResult ProductList()
        {
            using (var db = new HirentEntities())
            {

                return View();
            }
        }
        public ActionResult loadProduct()
        {
            using (var db = new HirentEntities())
            {
                int Permission = Helper.GetLocalAccountID();
                tb_WareHouse tb_WareHouse = db.tb_WareHouse.Where(m => m.LocalAccountID == Permission).FirstOrDefault();
                long WaseHouseID = tb_WareHouse!=null? tb_WareHouse.whId:0;
                var model = (from p in db.tb_Product.ToList()
                             join pt in db.tb_Product_Translation.ToList()
                             on p.ProductID equals long.Parse(pt.Idproduct.ToString()) into pts
                             from pts2 in pts.DefaultIfEmpty()
                             join w in db.tb_ProductTermConditionDetails
                             on p.ProductID equals w.ProductId
                             where (WaseHouseID == 0 || w.WarehouseId== WaseHouseID)
                             select new ProductInfo()
                             {
                                 ListBy1 = p.ListBy1,
                                 ListBy2 = p.ListBy2,
                                 ListBy3 = p.ListBy3,
                                 ListBy4 = p.ListBy4,
                                 ListBy5 = p.ListBy5,
                                 ListBy6 = p.ListBy6,
                                 ListBy7 = p.ListBy7,
                                 PricePerBlock = p.PricePerBlock,
                                 PricePerDay = p.PricePerDay,
                                 PricePerMonth = p.PricePerMonth,
                                 ProductAvatar = p.ProductAvatar,
                                 ProductCode = p.ProductCode,
                                 ProductID = p.ProductID,
                                 ProductName = p.ProductName,
                                 Soluongton = p.Soluongton,
                                 ProductName_EN = pts2 != null ? pts2.ProductName_EN : ""
                             }
                             ).OrderByDescending(m => m.ProductID).ToList();
                return PartialView(model);
            }
        }

        public ActionResult Product_edit(int? productID)
        {
            if (productID == null)
                productID = 0;
            using (var db = new HirentEntities())
            {
                var model = (from p in db.tb_Product
                             select new ProductSave()
                             {
                                 tb_Product = p,
                                 tb_ProductTermConditionDetails=db.tb_ProductTermConditionDetails.Where(m=>m.ProductId==p.ProductID).FirstOrDefault(),
                                 tb_ProductTermConditionDetails_Translation = db.tb_ProductTermConditionDetails_Translation.Where(m => m.ProductId == p.ProductID).FirstOrDefault()
                             }
                             ).Where(m => m.tb_Product.ProductID == productID).FirstOrDefault();
                //tb_ProductTermConditionDetails 
                var moacc = db.tb_ProductAccessorySelection.Where(m => m.ProductID == productID).ToList();
                List<objsub> lstobjsub = new List<objsub>();
                foreach (var item in moacc)
                {
                    objsub _objsub = new objsub();
                    tb_Product tb = db.tb_Product.Where(m => m.ProductName == item.ProductName).FirstOrDefault();
                    _objsub.id = int.Parse(tb.ProductID.ToString());
                    _objsub.name = tb.ProductName;
                    lstobjsub.Add(_objsub);

                }
                ViewBag.ListAcess = lstobjsub;
                if (productID != 0)
                    ViewBag.IdWareHouse = db.tb_ProductTermConditionDetails.Where(m => m.ProductId == productID).FirstOrDefault().WarehouseId;
                ViewBag.tb_ProductTermConditionDetails = db.tb_ProductTermConditionDetails.Where(m => m.ProductId == productID).FirstOrDefault();
                ViewBag.maincate = db.tb_CategoryMain.ToList();

                int localid = Helper.GetLocalAccountID();

                ViewBag.Warehouse = db.tb_WareHouse.Where(m=>m.LocalAccountID==localid || localid==1).ToList();
                ViewBag.tb_ProductPricePerHour = db.tb_ProductPricePerHour.Where(m => m.ProductID == productID).FirstOrDefault();
                ViewBag.tb_ProductPricePerDay = db.tb_ProductPricePerDay.Where(m => m.ProductID == productID).FirstOrDefault();
                ViewBag.tb_ProductPricePerMonth = db.tb_ProductPricePerMonth.Where(m => m.ProductID == productID).FirstOrDefault();
                ViewBag.tb_ProductGallery = db.tb_ProductGallery.Where(m => m.ProductID == productID).ToList();
                ViewBag.tb_Product_Translation = db.tb_Product_Translation.Where(m => m.Idproduct == productID).FirstOrDefault();
                ViewBag.lsttb_ProductCategorySelection = (from pd in db.tb_ProductCategorySelection.ToList()

                                                          join cm in db.tb_CategoryMain
                                                          on pd.ProductMainCate equals cm.MainCateID
                                                          join c1 in db.tb_CategorySub1.ToList()
                                                          on pd.ProductSubCate1 equals c1.SubCate1ID
                                                          join c2 in db.tb_CategorySub2.ToList()
                                                          on pd.ProductSubCate2 equals c2.SubCate2ID
                                                          select new ProductCategorySelection()
                                                          {
                                                              ProductId = int.Parse(pd.ProductId.ToString()),
                                                              ProductMainCateID = int.Parse(pd.ProductMainCate.ToString()),
                                                              ProductMainCateName = cm.MainCateName,
                                                              ProductSubCate1ID = int.Parse(c1.SubCate1ID.ToString()),
                                                              ProductSubCate1Name = c1.SubCate1Name,
                                                              ProductSubCate2ID = int.Parse(c2.SubCate2ID.ToString()),
                                                              ProductSubCate2Name = c2.SubCate2Name
                                                          }
                                                         ).Where(m => m.ProductId == productID).ToList();
                return View(model);
            }
        }

        public ActionResult PRoducteditcate1(int MainCateID)
        {
            using (var db = new HirentEntities())
            {
                var model = db.tb_CategorySub1.Where(m => m.MainCateID == MainCateID).ToList();
                return PartialView(model);
            }
        }
        public ActionResult PRoducteditcate2(int SubCate1ID)
        {
            using (var db = new HirentEntities())
            {
                var model = db.tb_CategorySub2.Where(m => m.SubCate1ID == SubCate1ID).ToList();
                return PartialView(model);
            }
        }
        [ValidateInput(false)]
        public void SaveData(string data)
        {
            using (var db = new HirentEntities())
            {
                ProductInfo model = JsonConvert.DeserializeObject<ProductInfo>(data);
                bool isAdd = false;
                tb_Product it;
                //

                if (model.ProductID == 0)
                {
                    isAdd = true;
                    it = new tb_Product();
                }
                else
                {
                    it = db.tb_Product.Where(m => m.ProductID == model.ProductID).FirstOrDefault();
                   // db.SaveChanges();
                }
                it.ProductCode = model.ProductCode;
                it.ProductName = model.ProductName;
                it.ProductAvatar = model.ProductAvatar;
                it.ShortDescription = model.ShortDescription;
                it.FullDescription = model.FullDescription;
                it.ProductSpecification = model.ProductSpecification;
                it.ProductInstruction = model.ProductInstruction;
                it.StatusPercentage = model.StatusPercentage;
                it.ProductValue = model.ProductValue;
                it.Soluongton = 10000;
                //
                it.PricePerBlock = model.PricePerBlock;
                it.PricePerDay = model.PricePerDay;
                it.PricePerMonth = model.PricePerMonth;
                it.Cocbangtienmat = model.Cocbangtienmat;
                it.Cocbanggiayto = model.Cocbanggiayto;
                it.Cocbanggiaytovahopdong = model.Cocbanggiaytovahopdong;
                it.Cocbanggiaytovahopdongprice = model.Cocbanggiaytovahopdongprice;
                it.Cocbangtaisan = model.Cocbangtaisan;
                if (isAdd)
                    db.tb_Product.Add(it);
                db.SaveChanges();
                //
                foreach (var item in model.lstsub.ToList())
                {
                    tb_ProductCategorySelection _tb_ProductCategorySelection = db.tb_ProductCategorySelection.Where(m => m.ProductId == it.ProductID && m.ProductMainCate == item.submain.id && m.ProductSubCate1 == item.sub1.id && m.ProductSubCate2 == item.sub2.id).FirstOrDefault();
                    if (_tb_ProductCategorySelection == null)
                    {
                        _tb_ProductCategorySelection = new tb_ProductCategorySelection();
                        _tb_ProductCategorySelection.ProductId = int.Parse(it.ProductID.ToString());
                        _tb_ProductCategorySelection.ProductMainCate = item.submain.id;
                        _tb_ProductCategorySelection.ProductSubCate1 = item.sub1.id;
                        _tb_ProductCategorySelection.ProductSubCate2 = item.sub2.id;
                        db.tb_ProductCategorySelection.Add(_tb_ProductCategorySelection);
                        db.SaveChanges();
                    }
                }
                //Xóa bị xóa
                var lstselect = db.tb_ProductCategorySelection.Where(m => m.ProductId == it.ProductID).ToList();
                var lstdeleteselecttion = lstselect.Where(m => !model.lstsub.Any(n => n.submain.id == m.ProductMainCate && n.sub1.id == m.ProductSubCate1 && n.sub2.id == m.ProductSubCate2)).ToList();

                foreach (var item in lstdeleteselecttion)
                {
                    var its = db.tb_ProductCategorySelection.Where(m => m.ProductMainCate == item.ProductMainCate && m.ProductSubCate1 == item.ProductSubCate1 && m.ProductSubCate2 == item.ProductSubCate2).FirstOrDefault();
                    if (its != null)
                    {
                        db.tb_ProductCategorySelection.Remove(its);
                        db.SaveChanges();
                    }
                }
                //tb_ProductPricePerHour
                var lstpriceblock = model.strPriceByBlock.Split(',');
                tb_ProductPricePerHour tb_ProductPricePerHour = db.tb_ProductPricePerHour.Where(m => m.ProductID == it.ProductID).FirstOrDefault();
                bool ispriceblock = false;
                if (tb_ProductPricePerHour == null)
                {
                    tb_ProductPricePerHour = new tb_ProductPricePerHour();
                    ispriceblock = true;
                }
                tb_ProductPricePerHour.ProductID = int.Parse(it.ProductID.ToString());
                tb_ProductPricePerHour.Block1 = double.Parse(lstpriceblock[0].ToString());
                tb_ProductPricePerHour.Block2 = double.Parse(lstpriceblock[1].ToString());
                tb_ProductPricePerHour.Block3 = double.Parse(lstpriceblock[2].ToString());
                tb_ProductPricePerHour.Block4 = double.Parse(lstpriceblock[3].ToString());
                tb_ProductPricePerHour.Block5 = double.Parse(lstpriceblock[4].ToString());
                tb_ProductPricePerHour.Block6 = double.Parse(lstpriceblock[5].ToString());
                tb_ProductPricePerHour.Block7 = double.Parse(lstpriceblock[6].ToString());
                tb_ProductPricePerHour.Block8 = double.Parse(lstpriceblock[7].ToString());
                tb_ProductPricePerHour.Block9 = double.Parse(lstpriceblock[8].ToString());
                tb_ProductPricePerHour.Block10 = double.Parse(lstpriceblock[9].ToString());
                tb_ProductPricePerHour.Block11 = double.Parse(lstpriceblock[10].ToString());
                tb_ProductPricePerHour.Block12 = double.Parse(lstpriceblock[11].ToString());
                tb_ProductPricePerHour.Block13 = double.Parse(lstpriceblock[12].ToString());
                tb_ProductPricePerHour.Block14 = double.Parse(lstpriceblock[13].ToString());
                tb_ProductPricePerHour.Block15 = double.Parse(lstpriceblock[14].ToString());
                tb_ProductPricePerHour.Block16 = double.Parse(lstpriceblock[15].ToString());
                tb_ProductPricePerHour.Block17 = double.Parse(lstpriceblock[16].ToString());
                tb_ProductPricePerHour.Block18 = double.Parse(lstpriceblock[17].ToString());
                tb_ProductPricePerHour.Block19 = double.Parse(lstpriceblock[18].ToString());
                tb_ProductPricePerHour.Block20 = double.Parse(lstpriceblock[19].ToString());
                tb_ProductPricePerHour.Block21 = double.Parse(lstpriceblock[20].ToString());
                tb_ProductPricePerHour.Block22 = double.Parse(lstpriceblock[21].ToString());
                tb_ProductPricePerHour.Block23 = double.Parse(lstpriceblock[22].ToString());
                tb_ProductPricePerHour.Block24 = double.Parse(lstpriceblock[23].ToString());
                if (ispriceblock)
                {
                    db.tb_ProductPricePerHour.Add(tb_ProductPricePerHour);
                }
                db.SaveChanges();
                //tb_ProductPricePerDay
                var lstpriceday = model.strPriceByDay.Split(',');
                tb_ProductPricePerDay tb_ProductPricePerDay = db.tb_ProductPricePerDay.Where(m => m.ProductID == it.ProductID).FirstOrDefault();
                bool ispriceday = false;
                if (tb_ProductPricePerDay == null)
                {
                    tb_ProductPricePerDay = new tb_ProductPricePerDay();
                    ispriceday = true;
                }
                tb_ProductPricePerDay.ProductID = int.Parse(it.ProductID.ToString());
                tb_ProductPricePerDay.Block1 = double.Parse(lstpriceday[0].ToString());
                tb_ProductPricePerDay.Block2 = double.Parse(lstpriceday[1].ToString());
                tb_ProductPricePerDay.Block3 = double.Parse(lstpriceday[2].ToString());
                tb_ProductPricePerDay.Block4 = double.Parse(lstpriceday[3].ToString());
                tb_ProductPricePerDay.Block5 = double.Parse(lstpriceday[4].ToString());
                tb_ProductPricePerDay.Block6 = double.Parse(lstpriceday[5].ToString());
                tb_ProductPricePerDay.Block7 = double.Parse(lstpriceday[6].ToString());
                tb_ProductPricePerDay.Block8 = double.Parse(lstpriceday[7].ToString());
                tb_ProductPricePerDay.Block9 = double.Parse(lstpriceday[8].ToString());
                tb_ProductPricePerDay.Block10 = double.Parse(lstpriceday[9].ToString());
                tb_ProductPricePerDay.Block11 = double.Parse(lstpriceday[10].ToString());
                tb_ProductPricePerDay.Block12 = double.Parse(lstpriceday[11].ToString());
                tb_ProductPricePerDay.Block13 = double.Parse(lstpriceday[12].ToString());
                tb_ProductPricePerDay.Block14 = double.Parse(lstpriceday[13].ToString());
                tb_ProductPricePerDay.Block15 = double.Parse(lstpriceday[14].ToString());
                tb_ProductPricePerDay.Block16 = double.Parse(lstpriceday[15].ToString());
                tb_ProductPricePerDay.Block17 = double.Parse(lstpriceday[16].ToString());
                tb_ProductPricePerDay.Block18 = double.Parse(lstpriceday[17].ToString());
                tb_ProductPricePerDay.Block19 = double.Parse(lstpriceday[18].ToString());
                tb_ProductPricePerDay.Block20 = double.Parse(lstpriceday[19].ToString());
                tb_ProductPricePerDay.Block21 = double.Parse(lstpriceday[20].ToString());
                tb_ProductPricePerDay.Block22 = double.Parse(lstpriceday[21].ToString());
                tb_ProductPricePerDay.Block23 = double.Parse(lstpriceday[22].ToString());
                tb_ProductPricePerDay.Block24 = double.Parse(lstpriceday[23].ToString());
                tb_ProductPricePerDay.Block25 = double.Parse(lstpriceday[24].ToString());
                tb_ProductPricePerDay.Block26 = double.Parse(lstpriceday[25].ToString());
                tb_ProductPricePerDay.Block27 = double.Parse(lstpriceday[26].ToString());
                tb_ProductPricePerDay.Block28 = double.Parse(lstpriceday[27].ToString());
                tb_ProductPricePerDay.Block29 = double.Parse(lstpriceday[28].ToString());
                tb_ProductPricePerDay.Block30 = double.Parse(lstpriceday[29].ToString());
                if (ispriceday)
                {
                    db.tb_ProductPricePerDay.Add(tb_ProductPricePerDay);
                }
                db.SaveChanges();
                //tb_ProductPricePerMonth
                var lstpricemonth = model.strPriceByDay.Split(',');
                tb_ProductPricePerMonth tb_ProductPricePerMonth = db.tb_ProductPricePerMonth.Where(m => m.ProductID == it.ProductID).FirstOrDefault();
                bool ispricemonth = false;
                if (tb_ProductPricePerMonth == null)
                {
                    tb_ProductPricePerMonth = new tb_ProductPricePerMonth();
                    ispricemonth = true;
                }
                tb_ProductPricePerMonth.ProductID = int.Parse(it.ProductID.ToString());
                tb_ProductPricePerMonth.Block1 = double.Parse(lstpricemonth[0].ToString());
                tb_ProductPricePerMonth.Block2 = double.Parse(lstpricemonth[1].ToString());
                tb_ProductPricePerMonth.Block3 = double.Parse(lstpricemonth[2].ToString());
                tb_ProductPricePerMonth.Block4 = double.Parse(lstpricemonth[3].ToString());
                tb_ProductPricePerMonth.Block5 = double.Parse(lstpricemonth[4].ToString());
                tb_ProductPricePerMonth.Block6 = double.Parse(lstpricemonth[5].ToString());
                tb_ProductPricePerMonth.Block7 = double.Parse(lstpricemonth[6].ToString());
                tb_ProductPricePerMonth.Block8 = double.Parse(lstpricemonth[7].ToString());
                tb_ProductPricePerMonth.Block9 = double.Parse(lstpricemonth[8].ToString());
                tb_ProductPricePerMonth.Block10 = double.Parse(lstpricemonth[9].ToString());
                tb_ProductPricePerMonth.Block11 = double.Parse(lstpricemonth[10].ToString());
                tb_ProductPricePerMonth.Block12 = double.Parse(lstpricemonth[11].ToString());
                if (ispricemonth)
                {
                    db.tb_ProductPricePerMonth.Add(tb_ProductPricePerMonth);
                }
                db.SaveChanges();
                //tb_ProductTermConditionDetails
                foreach (var pt in model.lstProductAccessorySelection.ToList())
                {
                    var chk = db.tb_ProductAccessorySelection.Where(m => m.ProductName == pt.name && m.ProductID == it.ProductID).FirstOrDefault();
                    if (chk == null)
                    {
                        tb_ProductAccessorySelection _tb_ProductAccessorySelection = new tb_ProductAccessorySelection();
                        _tb_ProductAccessorySelection.ProductID = it.ProductID;
                        _tb_ProductAccessorySelection.ProductName = pt.name;
                        db.tb_ProductAccessorySelection.Add(_tb_ProductAccessorySelection);
                        db.SaveChanges();
                    }
                }
                //Xóa tb_ProductTermConditionDetails
                var lstde = db.tb_ProductAccessorySelection.Where(m => m.ProductID == it.ProductID).ToList();
                List<objsub> lstobjsub = new List<objsub>();
                foreach (var its in lstde)
                {
                    objsub objsub = new objsub();
                    tb_Product tpduct = db.tb_Product.Where(m => m.ProductName == its.ProductName).FirstOrDefault();
                    objsub.id = int.Parse(tpduct.ProductID.ToString());
                    objsub.name = tpduct.ProductName;
                    lstobjsub.Add(objsub);
                }
                var lstdelete = lstobjsub.Where(m => !model.lstProductAccessorySelection.Any(n => n.id == m.id)).ToList();
                foreach (var idl in lstdelete)
                {
                    tb_ProductAccessorySelection tb_ProductAccessorySelection = db.tb_ProductAccessorySelection.Where(m => m.ProductID == it.ProductID && m.ProductName == idl.name).FirstOrDefault();
                    db.tb_ProductAccessorySelection.Remove(tb_ProductAccessorySelection);
                    db.SaveChanges();
                }

                //tb_ProductTermConditionDetails
                tb_ProductTermConditionDetails tb_ProductTermConditionDetails = db.tb_ProductTermConditionDetails.Where(m => m.ProductId == it.ProductID).FirstOrDefault();
                if (tb_ProductTermConditionDetails == null)
                {
                    tb_ProductTermConditionDetails = new tb_ProductTermConditionDetails();
                    tb_ProductTermConditionDetails.ProductId = it.ProductID;
                    tb_ProductTermConditionDetails.WarehouseId = model.WarehouseId;
                    tb_ProductTermConditionDetails.CostInstallation = model.CostInstallation;
                    tb_ProductTermConditionDetails.CostDelivery = model.CostDelivery;
                    tb_ProductTermConditionDetails.CostInstallAndDelivery = model.CostInstallAndDelivery;
                    tb_ProductTermConditionDetails.Tulapdat = model.Tulapdat;
                    tb_ProductTermConditionDetails.Huongdansudung = model.Huongdansudung;
                    tb_ProductTermConditionDetails.Thotoilap = model.Thotoilap;
                    tb_ProductTermConditionDetails.Thotoithao = model.Thotoithao;
                    tb_ProductTermConditionDetails.Thodenlapvathao = model.Thodenlapvathao;
                    tb_ProductTermConditionDetails.Tuvanchuyen = model.Tuvanchuyen;
                    tb_ProductTermConditionDetails.Giaotannha = model.Giaotannha;
                    tb_ProductTermConditionDetails.Nhantratannha = model.Nhantratannha;
                    tb_ProductTermConditionDetails.Giaovanhantratannha = model.Giaovanhantratannha;
                    db.tb_ProductTermConditionDetails.Add(tb_ProductTermConditionDetails);
                    db.SaveChanges();
                }
                else
                {
                    tb_ProductTermConditionDetails.WarehouseId = model.WarehouseId;
                    tb_ProductTermConditionDetails.CostInstallation = model.CostInstallation;
                    tb_ProductTermConditionDetails.CostDelivery = model.CostDelivery;
                    tb_ProductTermConditionDetails.CostInstallAndDelivery = model.CostInstallAndDelivery;
                    tb_ProductTermConditionDetails.Tulapdat = model.Tulapdat;
                    tb_ProductTermConditionDetails.Huongdansudung = model.Huongdansudung;
                    tb_ProductTermConditionDetails.Thotoilap = model.Thotoilap;
                    tb_ProductTermConditionDetails.Thotoithao = model.Thotoithao;
                    tb_ProductTermConditionDetails.Thodenlapvathao = model.Thodenlapvathao;
                    tb_ProductTermConditionDetails.Tuvanchuyen = model.Tuvanchuyen;
                    tb_ProductTermConditionDetails.Giaotannha = model.Giaotannha;
                    tb_ProductTermConditionDetails.Nhantratannha = model.Nhantratannha;
                    tb_ProductTermConditionDetails.Giaovanhantratannha = model.Giaovanhantratannha;
                    db.SaveChanges();

                }
                // tb_Product_Translation
                tb_Product_Translation tb_Product_Translation = db.tb_Product_Translation.Where(m => m.Idproduct == it.ProductID).FirstOrDefault();
                if (tb_Product_Translation == null)
                {
                    tb_Product_Translation = new tb_Product_Translation();
                    tb_Product_Translation.Idproduct = int.Parse(it.ProductID.ToString());
                    tb_Product_Translation.ShortDescription = model.ShortDescription_EN;
                    tb_Product_Translation.PricePerBlock_EN = model.PricePerBlock_EN;
                    tb_Product_Translation.PricePerDay_EN = model.PricePerDay_EN;
                    tb_Product_Translation.PricePerMonth_EN = model.PricePerMonth_EN;
                    tb_Product_Translation.ProductName_EN = model.ProductName_EN;
                    tb_Product_Translation.IdLanguage = "1";
                    db.tb_Product_Translation.Add(tb_Product_Translation);
                    db.SaveChanges();

                }
                else
                {
                    tb_Product_Translation.Idproduct = int.Parse(it.ProductID.ToString());
                    tb_Product_Translation.ShortDescription = model.ShortDescription_EN;
                    tb_Product_Translation.PricePerBlock_EN = model.PricePerBlock_EN;
                    tb_Product_Translation.PricePerDay_EN = model.PricePerDay_EN;
                    tb_Product_Translation.PricePerMonth_EN = model.PricePerMonth_EN;
                    tb_Product_Translation.ProductName_EN = model.ProductName_EN;
                    tb_Product_Translation.IdLanguage = "1";
                    db.SaveChanges();

                }
                //tb_ProductTermConditionDetails_Translation
                tb_ProductTermConditionDetails_Translation tb_ProductTermConditionDetails_Translation = db.tb_ProductTermConditionDetails_Translation.Where(m => m.ProductId == it.ProductID).FirstOrDefault();
                var add = false;
                if (tb_ProductTermConditionDetails_Translation == null)
                {
                    add = true;
                    tb_ProductTermConditionDetails_Translation = new tb_ProductTermConditionDetails_Translation();
                }
                tb_ProductTermConditionDetails_Translation.ProductId = int.Parse(model.ProductID.ToString());
                tb_ProductTermConditionDetails_Translation.CostInstallation = model.CostInstallationEN;
                tb_ProductTermConditionDetails_Translation.CostDelivery = model.CostDeliveryEN;
                tb_ProductTermConditionDetails_Translation.CostInstallAndDelivery = model.CostInstallAndDeliveryEN;
                tb_ProductTermConditionDetails_Translation.Tuvanchuyen = model.TuvanchuyenEN;
                tb_ProductTermConditionDetails_Translation.Giaotannha = model.GiaotannhaEN;
                tb_ProductTermConditionDetails_Translation.Nhantratannha= model.NhantratannhaEN;
                tb_ProductTermConditionDetails_Translation.Giaovanhantratannha = model.GiaovanhantratannhaEN;
                tb_ProductTermConditionDetails_Translation.Tulapdat = model.TulapdatEN;
                tb_ProductTermConditionDetails_Translation.Huongdansudung = model.HuongdansudungEN;
                tb_ProductTermConditionDetails_Translation.Thotoilap = model.ThotoilapEN;
                tb_ProductTermConditionDetails_Translation.Thotoithao = model.ThotoithaoEN;
                tb_ProductTermConditionDetails_Translation.Thodenlapvathao = model.ThodenlapvathaoEN;
                if (add)
                    db.tb_ProductTermConditionDetails_Translation.Add(tb_ProductTermConditionDetails_Translation);
                db.SaveChanges();
            }
        }

        public ActionResult Loadwarehouse(int WarehouseId)
        {
            using (var db = new HirentEntities())
            {
                var model = (from wh in db.tb_ProductTermConditionDetails.ToList()
                             join pd in db.tb_Product.ToList()
                             on wh.ProductId equals pd.ProductID
                             select new ProductInfo()
                             {
                                 WarehouseId = int.Parse(wh.WarehouseId.ToString()),
                                 ProductID = pd.ProductID,
                                 ProductName = pd.ProductName,
                                 ProductAvatar = pd.ProductAvatar
                             }
                             ).Where(m => m.WarehouseId == WarehouseId).ToList();
                return PartialView(model);
            }
        }

        #region Import Excel
        public string ImportExcel(string path)
        {
            string arraytotals = "";
            DataTable dt = new DataTable();
            string paths = "";
            string name = path;
            paths = System.Web.HttpContext.Current.Server.MapPath("~/image_product/") + name;
            bool firstRow = true;

            try
            {
                using (XLWorkbook workBook = new XLWorkbook(paths))
                {
                    //Đổ dữ liệu vào dataTable
                    IXLWorksheet workSheet = workBook.Worksheet(1);

                    foreach (IXLRow row in workSheet.Rows())
                    {
                        //Use the first row to add columns to DataTable.
                        if (firstRow)
                        {
                            foreach (IXLCell cell in row.Cells())
                            {
                                dt.Columns.Add(cell.Value.ToString());
                            }
                            firstRow = false;
                        }
                        else
                        {
                            //Add rows to DataTable.
                            dt.Rows.Add();
                            int i = 0;
                            var item = row.Cells();
                            foreach (IXLCell cell in row.Cells())
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                i++;
                            }
                        }
                    }
                    string arrayhour = "";
                    string arrayday = "";
                    string arraymonth = "";

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i < dt.Rows.Count - 1)
                        {
                            if (dt.Rows[0][1].ToString() != "")
                                arrayhour += dt.Rows[i][1].ToString() + "_";
                            if (dt.Rows[0][3].ToString() != "")
                                arrayday += dt.Rows[i][3].ToString() + "_";
                            if (dt.Rows[0][5].ToString() != "")
                                arraymonth += dt.Rows[i][5].ToString() + "_";
                        }

                        else
                        {
                            if (dt.Rows[0][1].ToString() != "")
                                arrayhour += dt.Rows[i][1].ToString();
                            if (dt.Rows[0][3].ToString() != "")
                                arrayday += dt.Rows[i][3].ToString();
                            if (dt.Rows[0][5].ToString() != "")
                                arraymonth += dt.Rows[i][5].ToString();
                        }
                    }
                    arraytotals = arrayhour + "." + arrayday + "." + arraymonth;
                }
            }
            catch (Exception ex)
            {

            }
            return arraytotals;
        }
        #endregion

        [HttpPost]
        public void SelectGroup(int id, int type)
        {
            using (var db = new HirentEntities())
            {
                var tb_product = db.tb_Product.Find(id);
                if (tb_product != null)
                {
                    if (type == 1)
                        tb_product.ListBy1 = tb_product.ListBy1 == "active" ? null : "active";
                    if (type == 2)
                        tb_product.ListBy2 = tb_product.ListBy2 == "active" ? null : "active";
                    if (type == 3)
                        tb_product.ListBy3 = tb_product.ListBy3 == "active" ? null : "active";
                    if (type == 4)
                        tb_product.ListBy4 = tb_product.ListBy4 == "active" ? null : "active";
                    if (type == 5)
                        tb_product.ListBy5 = tb_product.ListBy5 == "active" ? null : "active";
                    if (type == 6)
                        tb_product.ListBy6 = tb_product.ListBy6 == "active" ? null : "active";
                    if (type == 7)
                        tb_product.ListBy7 = tb_product.ListBy7 == "active" ? null : "active";
                    db.SaveChanges();
                }
            }
        }

        [HttpPost]
        public bool DeleteProduct(int id)
        {
            return true;
        }
    }
}