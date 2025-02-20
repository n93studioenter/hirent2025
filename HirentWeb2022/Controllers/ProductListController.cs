using HirentWeb2022.Models;
using HirentWeb2022.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HirentWeb2022.Controllers
{
    public class ProductListController : Controller
    {
        // GET: ProductList
        public ActionResult SearchProduct(int? MainCateID, int? ProductSubCate1, int? ProductSubCate2, int? page)
        {
            var getlang = Session["Lang"];
            if (getlang == null)
            {
                getlang = "vi";
            }
            ViewBag.lang = getlang;
            using (var db = new HirentEntities())
            {
                //Breeadcumb


                var model = (from p in db.tb_Product.ToList()
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
                                 ).OrderByDescending(m => m.tb_Product.ProductID).ToList();


                tb_CategorySub1 tb_CategorySub1 = new tb_CategorySub1();

                tb_CategorySub2 tb_CategorySub2 = new tb_CategorySub2();
                tb_CategoryMain tb_CategoryMain = new tb_CategoryMain();



                if (MainCateID.HasValue)
                {
                    tb_CategoryMain = db.tb_CategoryMain.Find(MainCateID);
                    model = model.Where(m => m.tb_ProductCategorySelection.ProductMainCate == MainCateID.Value).ToList();

                }
                if (ProductSubCate1.HasValue)
                {
                    tb_CategorySub1 = db.tb_CategorySub1.ToList().Where(m => m.SubCate1ID == ProductSubCate1.Value).FirstOrDefault();
                    model = model.Where(m => m.tb_ProductCategorySelection.ProductSubCate1 == ProductSubCate1.Value).ToList();
                    tb_CategoryMain = db.tb_CategoryMain.ToList().Where(m => m.MainCateID == tb_CategorySub1.MainCateID).FirstOrDefault();

                }
                if (ProductSubCate2.HasValue)
                {
                    tb_CategorySub2 = db.tb_CategorySub2.ToList().Where(m => m.SubCate2ID == ProductSubCate2.Value).FirstOrDefault();
                    tb_CategorySub1 = db.tb_CategorySub1.ToList().Where(m => m.SubCate1ID == tb_CategorySub2.SubCate1ID.Value).FirstOrDefault();
                   if(tb_CategorySub1!=null)
                    tb_CategoryMain = db.tb_CategoryMain.ToList().Where(m => m.MainCateID == tb_CategorySub1.MainCateID).FirstOrDefault();
                    model = model.Where(m => m.tb_ProductCategorySelection.ProductSubCate2 == ProductSubCate2.Value).ToList();
                }
                ViewBag.tb_CategorySub2 = tb_CategorySub2;

                ViewBag.tb_CategorySub1 = tb_CategorySub1;
                ViewBag.tb_CategoryMain = tb_CategoryMain;
                ViewBag.Count = model.Count();
                return View(model);

            }

        }
        public static string RemoveDiacritics(string input)
        {
            // Tạo một bảng chuyển đổi các ký tự có dấu sang không dấu
            string[] diacritics = { "á", "à", "ả", "ã", "ạ", "ă", "ằ", "ẳ", "ẵ", "ặ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ",
                            "é", "è", "ẻ", "ẽ", "ẹ", "ê", "ế", "ề", "ể", "ễ", "ệ",
                            "í", "ì", "ỉ", "ĩ", "ị",
                            "ó", "ò", "ỏ", "õ", "ọ", "ô", "ố", "ồ", "ổ", "ỗ", "ộ", "ơ", "ớ", "ờ", "ở", "ỡ", "ợ",
                            "ú", "ù", "ủ", "ũ", "ụ", "ư", "ứ", "ừ", "ử", "ữ", "ự",
                            "ý", "ỳ", "ỷ", "ỹ", "ỵ" };
            string[] noDiacritics = { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                            "e", "e", "e", "e", "e", "e", "e", "e", "e", "e", "e",
                            "i", "i", "i", "i", "i",
                            "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o",
                            "u", "u", "u", "u", "u", "u", "u", "u", "u", "u", "u",
                            "y", "y", "y", "y", "y" };

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                int index = Array.IndexOf(diacritics, c.ToString());
                if (index != -1)
                {
                    sb.Append(noDiacritics[index]);
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        public ActionResult TimSanPham(string keysearch, int? page)
        {
            var getlang = Session["Lang"];
            if (getlang == null)
            {
                getlang = "vi";
            }
            ViewBag.lang = getlang;
            using (var db = new HirentEntities())
            {
                var model = (from p in db.tb_Product.ToList()
                             join ps in db.tb_ProductCategorySelection.ToList()
                             on p.ProductID equals ps.ProductId.Value
                             join pe in db.tb_Product_Translation
                             on p.ProductID equals pe.Idproduct.Value
                             join td in db.tb_ProductTermConditionDetails
                             on p.ProductID equals td.ProductId.Value
                             join wh in db.tb_WareHouse
                             on td.WarehouseId equals wh.whId
                             where RemoveDiacritics(p.ProductName.ToLower()).Contains(RemoveDiacritics(keysearch))
                             select new ProductVM()
                             {
                                 tb_Product = p,
                                 tb_Product_Translation = pe,
                                 tb_ProductCategorySelection = ps,
                                 tb_WareHouse = wh
                             }
                                ).OrderByDescending(m => m.tb_Product.ProductID).ToList();
                ViewBag.Count = model.Count();
                return View(model);
            }

        }
    }
}