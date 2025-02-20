using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HirentWeb2022
{
	public static class Helper
	{
		public static string GetUsername()
		{

            if (HttpContext.Current != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["hirenadmin"];
                if (cookie != null)
                {
                    string username = cookie["username"];
                    return username;
                    ;
                }
            }
            return "";
        }
        public static int GetPermission()
        {

            if (HttpContext.Current != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["hirenadmin"];
                if (cookie != null)
                {
                    int PermissionID = int.Parse(cookie["PermissionID"]);
                    return PermissionID;
                }
            }
            return -1;
        }
        public static int GetLocalAccountID()
        {

            if (HttpContext.Current != null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["hirenadmin"];
                if (cookie != null)
                {
                    int LocalAccountID = int.Parse(cookie["LocalAccountID"]);
                    return LocalAccountID;
                }
            }
            return -1;
        }
    }
}