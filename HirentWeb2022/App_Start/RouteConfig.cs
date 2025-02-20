using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HirentWeb2022
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //blog-grid
            routes.MapRoute(
      name: "productdetail",
      url: "productdetail",
      defaults: new { controller = "Productdetail", action = "Getdetail" }
  );
            routes.MapRoute(
     name: "productcate",
     url: "productcate",
     defaults: new { controller = "ProductList", action = "SearchProduct" }
 );
            routes.MapRoute(
    name: "productsearch",
    url: "tim-kiem",
    defaults: new { controller = "ProductList", action = "TimSanPham" }
);
            routes.MapRoute(
   name: "Thanh toán",
   url: "order",
   defaults: new { controller = "Order", action = "YourCart2" }
);
            routes.MapRoute(
  name: "Quản lý đơn hàng",
  url: "ordermanagement",
  defaults: new { controller = "Order", action = "OderManagement" }
);
            routes.MapRoute(
   name: "User info",
   url: "userinfo",
   defaults: new { controller = "Partial", action = "Userinfor" }
);
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
