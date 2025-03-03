using DocumentFormat.OpenXml.Office.CustomUI;
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
    public class HomeController : Controller
    {
        public static List<ProductVM> Products { get; set; }
        public void DeleteOrder()
        {
            HirentEntities db = new HirentEntities();
            int customerId = 49;
            var lstOrder = db.tb_Pre_Order.Where(m => m.customerId == customerId).ToList();
            foreach(var item in lstOrder)
            {
                var getOrderDetail = db.tb_Pre_Order_Details.Where(m => m.pOrderId == item.pOrderId).FirstOrDefault();
                if (getOrderDetail!=null)
                {
                    db.tb_Pre_Order_Details.Remove(getOrderDetail);
                }
            }
            foreach (var item in lstOrder)
            {
                var getOrderaccompanying = db.tb_Pre_Order_accompanying.Where(m => m.pOrderId == item.pOrderId).FirstOrDefault();
                if (getOrderaccompanying != null)
                {
                    db.tb_Pre_Order_accompanying.Remove(getOrderaccompanying);
                }
            }
            foreach (var item in lstOrder)
            {
                db.tb_Pre_Order.Remove(item);
            }
            db.SaveChanges();
        }
        public void LevenshteinDistance()
        {
            string question = "mèo";
            string keyword = "mèo xanh"; 

            int[,] matrix = new int[keyword.Length + 1, question.Length + 1];

            for (int i = 0; i <= keyword.Length; i++)
            {
                matrix[i, 0] = i;
            }

            for (int j = 0; j <= question.Length; j++)
            {
                matrix[0, j] = j;
            }

            for (int i = 1; i <= keyword.Length; i++)
            {
                for (int j = 1; j <= question.Length; j++)
                {
                    int cost = (question[j - 1] == keyword[i - 1]) ? 0 : 1;
                    matrix[i, j] = Math.Min(Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1), matrix[i - 1, j - 1] + cost);
                }
            }

            int distance = matrix[keyword.Length, question.Length];

            if (distance == 0)
            {
                Console.WriteLine("Câu hỏi tương đồng với câu khóa.");
            }
            else
            {
                Console.WriteLine("Câu hỏi không tương đồng với câu khóa.");
            }
        }
        public string RemoveMark(string mark)
        {
            return mark.Replace("?", "").Replace("!", "");
        }
        public double Mathword(string question,string keyword)
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
                if (Array.Exists(keywordWords, element => element.ToLower() == word.ToLower()))
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
        static double SimilarityScore(string string1, string string2)
        {
            // Chuẩn hóa cả hai chuỗi về dạng chữ thường
            string1 = string1.ToLower();
            string2 = string2.ToLower();

            // Tính Levenshtein Distance
            double levenshteinDistance = LevenshteinDistance(string1, string2);

            // Độ dài lớn nhất giữa hai chuỗi
            int maxLength = Math.Max(string1.Length, string2.Length);

            // Tính similarity score dựa trên khoảng cách Levenshtein
            double similarity = 1 - (levenshteinDistance / maxLength);

            return similarity;
        }

        static int LevenshteinDistance(string string1, string string2)
        {
            int[,] distances = new int[string1.Length + 1, string2.Length + 1];

            for (int i = 0; i <= string1.Length; i++)
            {
                distances[i, 0] = i;
            }
            for (int j = 0; j <= string2.Length; j++)
            {
                distances[0, j] = j;
            }

            for (int j = 1; j <= string2.Length; j++)
            {
                for (int i = 1; i <= string1.Length; i++)
                {
                    if (string1[i - 1] == string2[j - 1])
                    {
                        distances[i, j] = distances[i - 1, j - 1];
                    }
                    else
                    {
                        distances[i, j] = Math.Min(distances[i - 1, j] + 1, Math.Min(distances[i, j - 1] + 1, distances[i - 1, j - 1] + 1));
                    }
                }
            }

            return distances[string1.Length, string2.Length];
        }
        public async Task<ActionResult> Index()
        { 

            var getlang = Session["Lang"];
            if (getlang == null)
            {
                getlang = "vi";
            }
            ViewBag.lang = getlang;
            HirentEntities db = new HirentEntities();
            ViewBag.ListSlide = db.tb_HomeMainBanner.OrderByDescending(m=>m.SortArr).ToList();
            ViewBag.ListCategory = db.tb_CategoryMain.ToList();

            var getlistprod = db.sp_GetProductList().ToList();
            List<tb_Product> productList = getlistprod.Select(p => new tb_Product
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                PricePerBlock = p.PricePerBlock,    
                PricePerDay = p.PricePerDay,    
                PricePerMonth= p.PricePerMonth,
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
            }).ToList();


            var query = (from p in productList
                         join ps in db.tb_ProductCategorySelection.ToList()
                         on p.ProductID equals ps.ProductId.Value
                         join pe in db.tb_Product_Translation
                         on p.ProductID equals pe.Idproduct.Value
                         join td in db.tb_ProductTermConditionDetails
                         on p.ProductID equals td.ProductId.Value
                         join wh in db.tb_WareHouse
                         on td.WarehouseId equals wh.whId
                         select new ProductVM()
                         {
                             tb_Product = p,
                             tb_Product_Translation = pe,
                             tb_ProductCategorySelection = ps,
                             tb_WareHouse = wh
                         }
                                 ).OrderByDescending(m => m.tb_Product.ProductID);

            Products = await Task.Run(() => query.ToList());

            return View();
        }
        public async Task<ActionResult> LoadProductByCate(int MainCateID, int Type)
        { 
           
            var getlang = Session["Lang"];
            if (getlang == null)
            {
                getlang = "vi";
            }
            ViewBag.lang = getlang;
            HirentEntities db = new HirentEntities();
            var query = from p in Products
                        where p.tb_ProductCategorySelection.ProductMainCate == MainCateID || MainCateID == 0 || (MainCateID == 0 && p.tb_ProductCategorySelection.ProductMainCate == null)
                        select p;
            var model = await Task.Run(() => query.ToList());
            if (Type == 1)
                model = model.Where(m => m.tb_Product.ListBy1 == "active").ToList();
            if (Type == 2)
                model = model.Where(m => m.tb_Product.ListBy2 == "active").ToList();
            if (Type == 3)
                model = model.Where(m => m.tb_Product.ListBy3 == "active").ToList();
            if (Type == 4)
                model = model.Where(m => m.tb_Product.ListBy4 == "active").ToList();
            if (Type == 5)
                model = model.Where(m => m.tb_Product.ListBy5 == "active").ToList();
            if (Type == 6)
                model = model.Where(m => m.tb_Product.ListBy6 == "active").ToList();
            if (Type == 7)
                model = model.Where(m => m.tb_Product.ListBy7 == "active").ToList();
            return  PartialView(model);
        }
        public bool Login(string userName, string passWord)
        {
            using (var db = new HirentEntities())
            {
                var checkLogin = db.tb_Customer.Where(m => m.Email.ToLower() == userName.ToLower() && m.Password == passWord).FirstOrDefault();
                if (checkLogin != null)
                {
                    HttpCookie userinfo1 = new HttpCookie("HirentLogin");
                    userinfo1["username"] = checkLogin.Email;
                    userinfo1.Expires = DateTime.Now.AddDays(2);
                    Response.Cookies.Add(userinfo1);
                    return true;
                }
                return false;
            }
        }
        public bool Register(string userName, string passWord)
        {
            try
            {
                using (var db = new HirentEntities())
                {
                    tb_Customer tb_Customer = new tb_Customer();
                    tb_Customer.Password = passWord;
                    tb_Customer.Email = userName;
                    db.tb_Customer.Add(tb_Customer);
                    db.SaveChanges();
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
           
        }
        public void Logout()
        {
            var cokie = Request.Cookies["HirentLogin"];
            cokie.Expires = DateTime.Now.AddDays(-366);
            cokie.Value = null;
            Response.Cookies.Add(cokie);
        }
        public ActionResult ProductDetail(int productID)
        {
            return View();
        }
    }
}