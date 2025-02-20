using DocumentFormat.OpenXml.Spreadsheet;
using HirentWeb2022.Models;
using HirentWeb2022.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HirentWeb2022.Controllers
{
    public class PartialController : Controller
    {
        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
    "đ",
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
    "í","ì","ỉ","ĩ","ị",
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
    "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
    "d",
    "e","e","e","e","e","e","e","e","e","e","e",
    "i","i","i","i","i",
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
    "u","u","u","u","u","u","u","u","u","u","u",
    "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }
        public ActionResult Search(string keyword)
        {
            var db = new HirentEntities();
            AutoCompleteSearch autoCompleteSearch = new AutoCompleteSearch();
            List<tb_Product> lstProduct = new List<tb_Product>();
            var getMaincate = db.tb_CategoryMain.ToList().Where(p => RemoveUnicode(p.MainCateName.ToLower()).Contains(RemoveUnicode(keyword.ToLower()))).Select(p => p).ToList();
            if (getMaincate != null)
            {
                foreach (var item in getMaincate)
                {
                    ProductCateSearch ProductCateSearch = new ProductCateSearch();
                    ProductCateSearch.Name = item.MainCateName;
                    ProductCateSearch.url = "/productcate?MainCateID=" + item.MainCateID;
                    autoCompleteSearch.productCateSearches.Add(ProductCateSearch);
                    //Addproduct
                    //var lsttb_ProductCategorySelection = db.tb_ProductCategorySelection.Where(m => m.ProductMainCate == item.MainCateID).ToList();
                    //foreach(var it in lsttb_ProductCategorySelection)
                    //{
                    //    var pd = db.tb_Product.Find(it.ProductId);
                    //    if (pd != null)
                    //    {
                    //        lstProduct.Add(pd);
                    //    }
                    //}
                    //lstProduct = lstProduct.Take(5).ToList();
                }
            }
            var getCateSub1 = db.tb_CategorySub1.ToList().Where(p => RemoveUnicode(p.SubCate1Name.ToLower()).Contains(RemoveUnicode(keyword.ToLower()))).Select(p => p).ToList();
            if (getCateSub1 != null)
            {
                foreach (var item in getCateSub1)
                {
                    ProductCateSearch ProductCateSearch = new ProductCateSearch();
                    ProductCateSearch.Name = item.SubCate1Name;
                    ProductCateSearch.url = "/productcate?ProductSubCate1=" + item.SubCate1ID;
                    autoCompleteSearch.productCateSearches.Add(ProductCateSearch);

                }
            }
            var getCateSub2 = db.tb_CategorySub2.ToList().Where(p => RemoveUnicode(p.SubCate2Name.ToLower()).Contains(RemoveUnicode(keyword.ToLower()))).Select(p => p).ToList();

            if (getCateSub2 != null)
            {
                foreach (var item in getCateSub2)
                {
                    ProductCateSearch ProductCateSearch = new ProductCateSearch();
                    ProductCateSearch.Name = item.SubCate2Name;
                    ProductCateSearch.url = "/productcate?ProductSubCate2=" + item.SubCate2ID;
                    autoCompleteSearch.productCateSearches.Add(ProductCateSearch);
                }
            }
            //Nếu ko có danh mục thì lấy sản phẩm dựa theo search

            if (autoCompleteSearch.productCateSearches.Count == 0)
            {
                lstProduct = db.tb_Product.ToList().Where(m => RemoveUnicode(m.ProductName.ToLower()).Contains(RemoveUnicode(keyword.ToLower()))).ToList();
            }

            if (lstProduct != null)
            {
                foreach (var item in lstProduct)
                {
                    ProductSeacch productSeacch = new ProductSeacch();
                    productSeacch.Name = item.ProductName;
                    productSeacch.url = "/ProductDetail?productID=" + item.ProductID;
                    productSeacch.Images = item.ProductAvatar;
                    autoCompleteSearch.productSearches.Add(productSeacch);
                }
            }
            autoCompleteSearch.productCateSearches = autoCompleteSearch.productCateSearches.GroupBy(m => m.Name).SelectMany(f => f.Take(1)).ToList();
            return PartialView(autoCompleteSearch);
        }
        // GET: Partial
        public ActionResult Menutop()
        {

            var getlang = Session["Lang"];
            if (getlang == null)
            {
                getlang = "vi";
                Session["Lang"] = "vi";
            }
            ViewBag.lang = getlang;
            using (var db = new HirentEntities())
            {
                ViewBag.tb_CategoryMain = db.tb_CategoryMain.OrderBy(m=>m.Sort).ToList();
            }
            return PartialView();
        }
        public ActionResult Footer()
        {
            var getlang = Session["Lang"];
            if (getlang == null)
            {
                getlang = "vi";
                Session["Lang"] = "vi";
            }
            ViewBag.lang = getlang;
            return PartialView();
        }

        public ActionResult MenuSub1(int MainCateID)
        {
            var getlang = Session["Lang"];
            if (getlang == null)
            {
                getlang = "vi";
                Session["Lang"] = "vi";
            }
            ViewBag.lang = getlang;
            using (var db = new HirentEntities())
            {
                ViewBag.tb_CategorySub1 = db.tb_CategorySub1.Where(m => m.MainCateID == MainCateID).ToList();
            }
            return PartialView();
        }
        public ActionResult MenuMobileSub1(int MainCateID)
        {
            var getlang = Session["Lang"];
            if (getlang == null)
            {
                getlang = "vi";
                Session["Lang"] = "vi";
            }
            ViewBag.lang = getlang;
            using (var db = new HirentEntities())
            {
                var model = db.tb_CategorySub1.Where(m => m.MainCateID == MainCateID).ToList();
                return PartialView(model);
            }

        }
        public ActionResult MenuSub2(int SubCate1ID)
        {
            var getlang = Session["Lang"];
            if (getlang == null)
            {
                getlang = "vi";
                Session["Lang"] = "vi";
            }
            ViewBag.lang = getlang;
            using (var db = new HirentEntities())
            {
                ViewBag.tb_CategorySub2 = db.tb_CategorySub2.Where(m => m.SubCate1ID == SubCate1ID).ToList();
            }
            return PartialView();
        }
        public void setLanguage(string lang)
        {
            if (lang == "vi")
                Session["Lang"] = "vi";
            else
                Session["Lang"] = "en";
        }
        public ActionResult LoadMenu()
        {
            var getlang = Session["Lang"];
            if (getlang == null)
            {
                getlang = "vi";
                Session["Lang"] = "vi";
            }
            ViewBag.lang = getlang;

            HttpCookie reqCookies = Request.Cookies["HirentLogin"];
            if (reqCookies != null)
            {
                ViewBag.UserName = reqCookies["username"].ToString();
            }
            return PartialView();
        }

        public ActionResult Helper()
        {
            return PartialView();
        }

        public string RemoveMark(string mark)
        {
            return mark.Replace("?", "").Replace("!", "");
        }

        public double Mathword(string question, string keyword)
        {
            //Loại bỏ ?,!
            question = RemoveMark(question);
            keyword = RemoveMark(keyword);
            // Tách câu thành các từ
            string[] questionWords = question.Split(' ');
            string[] keywordWords = null;

            keywordWords = keyword.Split(' ');
            int matchCount = 0;

            // So sánh từng từ
            foreach (var word in questionWords)
            {
                if (Array.Exists(keywordWords, element => RemoveUnicode(element.ToLower()) == RemoveUnicode(word.ToLower())))
                {
                    matchCount++;
                }
            }

            // Tính toán độ tương đồng
            double similarity = (double)matchCount / keywordWords.Length;

            // In ra kết quả
            if (similarity >= 0.5)
            {
                Console.WriteLine("Câu hỏi tương đồng với câu khóa.");
            }
            else
            {
                Console.WriteLine("Câu hỏi không tương đồng với câu khóa.");
            }
            return similarity;
        }

        [HttpPost]
        //public ActionResult Botanswer(string question)
        //{
        //    List<VM_Chatbot> lstVmChatbot = new List<VM_Chatbot>();
        //    using (HirentEntities db = new HirentEntities())
        //    {
        //        var lstKeywords = db.tb_Chatbot.ToList();
        //        foreach (var word in lstKeywords)
        //        {
        //            var getSplitKeywords = word.Question.Split(',');
        //            foreach (var spl in getSplitKeywords)
        //            {
        //                string keyword = spl;
        //                var resultr = Mathword(question, keyword);
        //                if (resultr >= 0.5)
        //                {
        //                    VM_Chatbot vM_Chatbot = new VM_Chatbot();
        //                    vM_Chatbot.Answer = word.Answer;
        //                    vM_Chatbot.Similarity = resultr;
        //                    lstVmChatbot.Add(vM_Chatbot);
        //                }
        //            }

        //        }
        //        VM_Chatbot result = null;
        //        if (lstVmChatbot.Count() > 0)
        //        {
        //            result = lstVmChatbot.OrderByDescending(item => item.Similarity).FirstOrDefault();
        //        }

        //        return PartialView(result);
        //    }
        //}
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult Userinfor()
        {
            var getlang = Session["Lang"];
            if (getlang == null)
            {
                getlang = "vi";
                Session["Lang"] = "vi";
            }
            ViewBag.lang = getlang;
            HttpCookie reqCookies = Request.Cookies["HirentLogin"];
            if (reqCookies != null)
            {
                string getEmail = reqCookies["username"].ToString();
                HirentEntities db = new HirentEntities();
                tb_Customer tb_Customer = db.tb_Customer.Where(m => m.Email == getEmail).FirstOrDefault();
                if (tb_Customer != null)
                {
                    ViewBag.Info = tb_Customer;
                    @ViewBag.CustomerID= tb_Customer.CustomerID;
                    ViewBag.Address = db.tb_CustomerDeliveryAddress.Where(m => m.CustomerID == tb_Customer.CustomerID && m.IsMacdinh==1).FirstOrDefault();
                    ViewBag.ListAddress = db.tb_CustomerDeliveryAddress.Where(m => m.CustomerID == tb_Customer.CustomerID).ToList();
                    return View();
                }

            }
            return Redirect("/");
        }

        [HttpPost]
        public async Task<bool> UpdateUserInfo(tb_Customer tb_Customer)
        {
            try
            {
                using (var db = new HirentEntities())
                {
                    tb_Customer findCustomer = db.tb_Customer.Find(tb_Customer.CustomerID);
                    if (findCustomer != null)
                    {
                        findCustomer.Email = tb_Customer.Email;
                        findCustomer.FirstName = tb_Customer.FirstName;
                        findCustomer.Phone = tb_Customer.Phone; 
                        findCustomer.Birthday = tb_Customer.Birthday;
                        if (db.tb_CustomerDeliveryAddress.Any(m => m.CustomerID == findCustomer.CustomerID && m.IsMacdinh==1))
                        {
                            tb_CustomerDeliveryAddress tb_CustomerDeliveryAddress = db.tb_CustomerDeliveryAddress.Where(m => m.CustomerID == findCustomer.CustomerID && m.IsMacdinh == 1).FirstOrDefault();
                            if (tb_CustomerDeliveryAddress != null)
                            {
                                tb_CustomerDeliveryAddress.Address= tb_Customer.Address;
                            }
                        }
                        else
                        {
                            tb_CustomerDeliveryAddress tb_CustomerDeliveryAddress = new tb_CustomerDeliveryAddress();
                            tb_CustomerDeliveryAddress.Address= tb_Customer.Address;
                            tb_CustomerDeliveryAddress.CustomerID=findCustomer.CustomerID;
                            tb_CustomerDeliveryAddress.CreateDate = DateTime.Now;
                            tb_CustomerDeliveryAddress.FullName = findCustomer.FirstName;
                            tb_CustomerDeliveryAddress.PhoneNumber = findCustomer.Phone;
                            tb_CustomerDeliveryAddress.IsMacdinh = 1;
                            db.tb_CustomerDeliveryAddress.Add(tb_CustomerDeliveryAddress);
                        }
                        await db.SaveChangesAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        
        public ActionResult ChangePassword()
        {
            var getlang = Session["Lang"];
            if (getlang == null)
            {
                getlang = "vi";
                Session["Lang"] = "vi";
            }
            ViewBag.lang = getlang;
            HttpCookie reqCookies = Request.Cookies["HirentLogin"];
            if (reqCookies != null)
            {
               
                string getEmail = reqCookies["username"].ToString();
                HirentEntities db = new HirentEntities();
                tb_Customer tb_Customer = db.tb_Customer.Where(m => m.Email == getEmail).FirstOrDefault();
                if (tb_Customer != null)
                {
                    ViewBag.Info = tb_Customer;

                }
            }
           
            return View();
        }

        public bool Checkpassword(int CustomerID,string currentPassword)
        {
            using (var db = new HirentEntities())
            {
                var check = db.tb_Customer.Find(CustomerID);
                if (check != null)
                {
                    if(check.Password!=currentPassword)
                    return true;
                }
               
            }
            return false;
        }
        public bool UpdatePasswod(int CustomerID, string newPassword)
        {
            using (var db = new HirentEntities())
            {
                var check = db.tb_Customer.Find(CustomerID);
                if (check != null)
                {
                    check.Password = newPassword;
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public ActionResult CustomerAddressInfo()
        {
            var getlang = Session["Lang"];
            if (getlang == null)
            {
                getlang = "vi";
                Session["Lang"] = "vi";
            }
            ViewBag.lang = getlang;
            var db = new HirentEntities();
            ViewBag.Listprovinces = db.provinces.ToList();
            return PartialView();
        }

        public ActionResult ChangeProvinces(string code)
        {
            var getlang = Session["Lang"];
            if (getlang == null)
            {
                getlang = "vi";
                Session["Lang"] = "vi";
            }
            ViewBag.lang = getlang;
            var db = new HirentEntities();
            var model=db.districts.Where(m=>m.province_code==code).ToList();

            return PartialView(model);
        }
        public ActionResult ChangeeDistricts(string code)
        {
            var getlang = Session["Lang"];
            if (getlang == null)
            {
                getlang = "vi";
                Session["Lang"] = "vi";
            }
            ViewBag.lang = getlang;
            var db = new HirentEntities();
            var model = db.wards.Where(m => m.district_code == code).ToList();

            return PartialView(model);
        }
    }
}