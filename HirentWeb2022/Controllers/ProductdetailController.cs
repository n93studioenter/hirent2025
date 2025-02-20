using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using DocumentFormat.OpenXml.Drawing.Charts;
using HirentWeb2022.Models;
namespace HirentWeb2022.Controllers
{
    public class ProductdetailController : Controller
    {
        // GET: Productdetail 
        public ActionResult Getdetail(int productID)
        {
            using (HirentEntities db = new HirentEntities())
            {
                var getlistprod = db.sp_GetProductList().ToList();
                List<tb_Product> productList = getlistprod.Select(p => new tb_Product
                {
                    ProductID = p.ProductID,
                    ProductName = p.ProductName,
                    PricePerBlock = p.PricePerBlock,
                    PricePerDay = p.PricePerDay,
                    PricePerMonth = p.PricePerMonth,
                    ProductCode = p.ProductCode,
                    ProductValue = p.ProductValue,
                    ListBy1 = p.ListBy1,
                    ListBy2 = p.ListBy2,
                    ListBy3 = p.ListBy3,
                    ListBy4 = p.ListBy4,
                    ListBy5 = p.ListBy5,
                    ListBy6 = p.ListBy6,
                    ListBy7 = p.ListBy7,
                    StatusPercentage = p.StatusPercentage,
                    ProductAvatar = p.ProductAvatar,
                    Soluongton=p.Soluongton,
                    ShortDescription = p.ShortDescription,
                    FullDescription = p.FullDescription,
                    ProductSpecification = p.ProductSpecification,
                    ProductInstruction= p.ProductInstruction
                }).ToList();
                ViewBag.productID = productID;
                var model = productList.Where(m => m.ProductID == productID).ToList();
                ViewBag.Translate = db.tb_Product_Translation.ToList().Where(m => m.Idproduct == productID).FirstOrDefault();
                //Main
                var main = db.tb_ProductCategorySelection.Where(m => m.ProductId == productID).FirstOrDefault();
                var getmainname = db.tb_CategoryMain.Where(m => m.MainCateID == main.ProductMainCate).FirstOrDefault();
                ViewBag.getmainname = getmainname;
                // Sub 1
                var getmainname1 = db.tb_CategorySub1.Where(m => m.SubCate1ID == main.ProductSubCate1).FirstOrDefault();
                ViewBag.getmainname1 = getmainname1;
                //Sub 2
                var getmainname2 = db.tb_CategorySub2.Where(m => m.SubCate2ID == main.ProductSubCate2).FirstOrDefault();
                ViewBag.getmainname2 = getmainname2;
                //
                var getlang = Session["Lang"];
                if (getlang == null)
                    getlang = "vi";
                ViewBag.lang = getlang;

                ViewBag.tb_ProductGallery = db.tb_ProductGallery.Where(m => m.ProductID == productID).ToList();
                //
                tb_ProductTermConditionDetails tb_ProductTermConditionDetails = db.tb_ProductTermConditionDetails.Where(m => m.ProductId == productID).FirstOrDefault();
                var lstWht = db.tb_WareHouse_Time.Where(m => m.whId == tb_ProductTermConditionDetails.WarehouseId).ToList();
                //
                string fromdate = "";
                foreach (var item in lstWht)
                {
                    fromdate += item.FromDayOfWeek;
                }
                ViewBag.Fromdate = fromdate;
                ViewBag.ListWTime = lstWht;
                tb_WareHouse tb_WareHouse = db.tb_WareHouse.Where(m => m.whId == tb_ProductTermConditionDetails.WarehouseId).FirstOrDefault();
                ViewBag.tb_WareHouse = tb_WareHouse;
                ViewBag.ListTime = db.tb_WareHouse_Time.Where(m => m.whId == tb_WareHouse.whId).ToList();
                var HirentEntities= db.tb_ProductAccessorySelection.Where(m => m.ProductID == productID).ToList();
                ViewBag.ListAcess = (from m in HirentEntities
                              join p in productList
                              on m.ProductCode equals p.ProductCode
                              join te in db.tb_ProductTermConditionDetails
                              on p.ProductID equals te.ProductId
                              where te.WarehouseId == tb_ProductTermConditionDetails.WarehouseId
                              select p
                              ).OrderBy(m=>m.ProductID).ToList();
                ViewBag.Listmaincate = db.tb_CategoryMain.ToList();
                return View(model);
            }
        }
        [HttpPost]
        //1 Thành công
        //-1 Thất Bại
        //0 Chưa Đăng Nhập
        public int AddToCart()
        {
            HttpCookie reqCookies = Request.Cookies["HirentLogin"];
            if (reqCookies != null)
            {
                var email = reqCookies["username"].ToString();
                using(var db=new HirentEntities())
                {
                    tb_Customer tb_Customer = db.tb_Customer.Where(m => m.Email == email).FirstOrDefault();
                    if(tb_Customer != null)
                    {
                        System.Data.DataTable dataTable = new System.Data.DataTable();
                        dataTable = (System.Data.DataTable)Session["Order"];
                        if (dataTable != null)
                        {
                            tb_Pre_Order tb_Pre_Order = new tb_Pre_Order();
                            foreach (DataRow row in dataTable.Rows)
                            {
                                int id = (int)row["productId"];
                                int type = (int)row["Type"];
                                int rentalType = (int)row["rentalType"];
                                int Qty = (int)row["Qty"];
                                var total = (float)row["Totals"];
                                DateTime timePickup = (DateTime)row["FromDate"];
                                DateTime timeReturn = (DateTime)row["ToDate"];
                                int pOrderId = 0;
                                //Sản phẩm chính
                                if (type == 1)
                                {

                                    //Insert Table tb_Pre_Order
                                  
                                    tb_Pre_Order.customerId = tb_Customer.CustomerID;
                                    tb_Pre_Order.productId = id;
                                    tb_Pre_Order.status = 1;
                                    tb_Pre_Order.createTime = DateTime.Now;
                                    tb_Pre_Order.Totals = 0;
                                    db.tb_Pre_Order.Add(tb_Pre_Order);
                                    db.SaveChanges();

                                    //Insert Table tb_Pre_Order_Details
                                    tb_Pre_Order_Details tb_Pre_Order_Details = new tb_Pre_Order_Details();
                                    tb_Pre_Order_Details.pOrderId = tb_Pre_Order.pOrderId;
                                    tb_Pre_Order_Details.rentalType = rentalType;
                                    tb_Pre_Order_Details.productQty = Qty;
                                    tb_Pre_Order_Details.timePickup = timePickup;
                                    tb_Pre_Order_Details.timeReturn = timeReturn;
                                    tb_Pre_Order_Details.OrderCode = DateTime.Now.ToString("yyyyMMddHHmmss");
                                    tb_Pre_Order_Details.Totals = total;
                                    db.tb_Pre_Order_Details.Add(tb_Pre_Order_Details);
                                    db.SaveChanges();
                                    Session["Order"] = null;
                                }
                                //Sản phẩm phụ
                                else
                                {
                                    //Insert Table tb_Pre_Order_accompanying
                                    tb_Pre_Order_accompanying tb_Pre_Order_accompanying = new tb_Pre_Order_accompanying();
                                    tb_Pre_Order_accompanying.pOrderId = tb_Pre_Order.pOrderId;
                                    tb_Pre_Order_accompanying.accompanyingQty = Qty;
                                    tb_Pre_Order_accompanying.productId = id;
                                    tb_Pre_Order_accompanying.createTimeClone = DateTime.Now;
                                    tb_Pre_Order_accompanying.Totals = total;
                                    db.tb_Pre_Order_accompanying.Add(tb_Pre_Order_accompanying);
                                    db.SaveChanges();
                                }

                            }
                            return tb_Pre_Order.pOrderId;
                        }
                    }
                }
               
            }
            else
            {
                return 0;
            }
           
            return 1;
        }
        public string Capnhatxuly(string FromDate,string ToDate,int chkChon,int productId,int type,int Qty)
        {
            var db = new HirentEntities();
            var sp1 = FromDate.Split(',');
            var sp11 = sp1[0].Split('/');
            string result = "";
            string thoigian = "";
            DateTime tn = new DateTime(int.Parse(sp11[2]), int.Parse(sp11[1]), int.Parse(sp11[0]), int.Parse(sp1[1]), int.Parse(sp1[2]), 0);
            //
            var kp1 = ToDate.Split(',');
            var kp11 = kp1[0].Split('/');
            DateTime dn = new DateTime(int.Parse(kp11[2]), int.Parse(kp11[1]), int.Parse(kp11[0]), int.Parse(kp1[1]), int.Parse(kp1[2]), 0);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            if (Session["Order"] == null)
            {
                dataTable.Columns.Add("ProductID", typeof(int));
                dataTable.Columns.Add("Qty", typeof(int));
                dataTable.Columns.Add("Type", typeof(int));
                dataTable.Columns.Add("rentalType", typeof(int));
                dataTable.Columns.Add("FromDate", typeof(DateTime));
                dataTable.Columns.Add("ToDate", typeof(DateTime));
                dataTable.Columns.Add("Totals", typeof(float));
            }
            else
            {
                dataTable = (System.Data.DataTable)Session["Order"];
            }
          
           
            DateTime ngaybatdau = tn;
            DateTime ngayketthuc = dn;
            TimeSpan totalday = ngayketthuc - ngaybatdau;
            int TongSoNgay = totalday.Days;
            int TongSoGio = totalday.Hours;
            var TongSoThang = totalday.Days / 30;
            var thangdu = TongSoNgay - TongSoThang * 30;
            if (TongSoThang != 0)
            {
                if (thangdu != 0)
                    TongSoThang += 1;
            }
           
            //Theo giờ
            double GiaTienTheoGio = 0;
            double Giatienmotngay = 0;
            double TotaltienTheogio = 0;

            //Theo Ngày
            double GiaTienTheoNgay = 0;
            double Giatienmotngay2 = 0;
            //Theo tháng
            //Trường hợp chọn theo giờ
            if (chkChon == 1)
            {
                if (TongSoGio > 0)
                    GiaTienTheoGio = double.Parse(db.tb_ProductPricePerHour.Where(m => m.ProductID == productId).FirstOrDefault().GetType().GetProperty("Block" + TongSoGio).GetValue(db.tb_ProductPricePerHour.Where(m => m.ProductID == productId).FirstOrDefault()).ToString());


                //Trường hợp chưa tới 1 ngày
                if (TongSoNgay > 0)
                {
                    //Tính tổng tiền theo số ngày dư
                    //Tiền của một ngày

                    Giatienmotngay = double.Parse(db.tb_ProductPricePerHour.Where(m => m.ProductID == productId).FirstOrDefault().GetType().GetProperty("Block" + 24).GetValue(db.tb_ProductPricePerHour.Where(m => m.ProductID == productId).FirstOrDefault()).ToString());

                }
                //Tính thời gian
                thoigian = TongSoGio + TongSoNgay*24+" Giờ";
                TotaltienTheogio = GiaTienTheoGio + Giatienmotngay * TongSoNgay;
                result= TotaltienTheogio.ToString()+","+ thoigian;
            }
            else if (chkChon == 2)
            {
                int songaydu = 0;
                if (TongSoNgay > 30)
                {
                    songaydu = TongSoNgay - 30;
                    TongSoNgay = 30;

                }
                if (TongSoGio > 0)
                    if (TongSoNgay < 30)
                    {
                        TongSoNgay += 1;
                    }
                    else
                    {
                        songaydu += 1;
                    }
                try
                {
                    GiaTienTheoNgay += double.Parse(db.tb_ProductPricePerDay.Where(m => m.ProductID == productId).FirstOrDefault().GetType().GetProperty("Block" + TongSoNgay).GetValue(db.tb_ProductPricePerDay.Where(m => m.ProductID == productId).FirstOrDefault()).ToString());
                    if (songaydu > 0)
                        GiaTienTheoNgay += double.Parse(db.tb_ProductPricePerDay.Where(m => m.ProductID == productId).FirstOrDefault().GetType().GetProperty("Block" + songaydu).GetValue(db.tb_ProductPricePerDay.Where(m => m.ProductID == productId).FirstOrDefault()).ToString());

                    TotaltienTheogio = GiaTienTheoNgay + Giatienmotngay2;
                    thoigian = TongSoNgay + songaydu  + " Ngày";
                }
                catch (Exception ex)
                {

                }
                result= TotaltienTheogio.ToString() + "," + thoigian;
            }
            else
            {
                if (TongSoThang == 0)
                    TongSoThang = 1;
                try
                {
                    if(db.tb_ProductPricePerMonth.Where(m => m.ProductID == productId).FirstOrDefault() != null)
                    {
                        TotaltienTheogio += double.Parse(db.tb_ProductPricePerMonth.Where(m => m.ProductID == productId).FirstOrDefault().GetType().GetProperty("Block" + TongSoThang).GetValue(db.tb_ProductPricePerMonth.Where(m => m.ProductID == productId).FirstOrDefault()).ToString());
                        thoigian = TongSoThang + " Tháng";
                    }
                    
                }
                catch (Exception ex)
                {

                }
                result= TotaltienTheogio.ToString() + "," + thoigian;
            }


            //Kiểm tra xem datatable đã có sản phẩm này chưa
            int hasProductid = -1;
            if (dataTable != null)
            {
                int index = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    int id = (int)row["productId"];
                    if (id == productId)
                    {
                        hasProductid = index;
                    }
                    index += 1;
                }
            }
            if (hasProductid == -1)
            {
                DataRow row1 = dataTable.NewRow();
                row1["ProductID"] = productId;
                row1["Qty"] = Qty;
                row1["Type"] = type;
                row1["rentalType"] = chkChon;
                row1["FromDate"] = tn;
                row1["ToDate"] = dn;
                row1["Totals"] = TotaltienTheogio;
                dataTable.Rows.Add(row1);
            }
            else
            {
                dataTable.Rows[hasProductid]["Qty"] = Qty;
                dataTable.Rows[hasProductid]["Totals"] = TotaltienTheogio;
            }
            Session["Order"] = dataTable;
            return result;
        }
        
        public ActionResult ProductShopList(int MainCateID)
        {
            var getlang = Session["Lang"];
            if (getlang == null)
                getlang = "vi";
            ViewBag.lang = getlang;
            HirentEntities db = new HirentEntities();
            var model = (from p in db.tb_Product
                         join m in db.tb_ProductCategorySelection
                         on p.ProductID equals m.ProductId.Value
                         where (MainCateID==0 || m.ProductMainCate.Value == MainCateID)
                         select p
                       ).ToList();
            return PartialView(model);
        }
        public ActionResult ProductAddmore(string id)
        {
            var getlang = Session["Lang"];
            if (getlang == null)
                getlang = "vi";
            ViewBag.lang = getlang;
            var lstSplit = id.Split(',');
            List<tb_Product> lst = new List<tb_Product>();
            HirentEntities db = new HirentEntities();
            foreach (var it in lstSplit)
            {
                if (it != "," && !string.IsNullOrEmpty(it))
                {
                    tb_Product tb_Product =db.tb_Product.ToList().Where(m=>m.ProductID==int.Parse(it)).FirstOrDefault();
                    if(tb_Product != null)
                    {
                        lst.Add(tb_Product);
                    }
                }
            }
            return PartialView(lst);
        }

        public ActionResult WasehouseTime(int whId)
        {
            var getlang = Session["Lang"];
            if (getlang == null)
                getlang = "vi";
            ViewBag.lang = getlang;
            HirentEntities db = new HirentEntities();
            ViewBag.ListTime = db.tb_WareHouse_Time.Where(m => m.whId == whId).ToList();
            return PartialView();
        }
    }
}