<%@ WebHandler Language="C#" Class="UploadProductGallery" %>

using System;
using System.Web;
using System.IO;

public class UploadProductGallery : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        try
        {
            string dirFullPath = HttpContext.Current.Server.MapPath("~/image_product/");
            string[] files;
            int numFiles;
            files = System.IO.Directory.GetFiles(dirFullPath);
            numFiles = files.Length;
            numFiles = numFiles + 1;
            string str_image = "";

            foreach (string s in context.Request.Files)
            {
                HttpPostedFile file = context.Request.Files[s];
                string fileName = file.FileName;
                string fileExtension = file.ContentType;

                if (!string.IsNullOrEmpty(fileName))
                {
                    fileExtension = Path.GetExtension(fileName);
                    str_image = "Product-gallery-" +  DateTime.Now.ToString("yyyyMMddhhmmssff") + fileExtension;
                    string pathToSave_100 = HttpContext.Current.Server.MapPath("~/image_product/") + str_image;
                    file.SaveAs(pathToSave_100);
                }
            }
            //  database record update logic here  ()
            
            context.Response.Write(str_image);
        }
        catch (Exception) 
        { 
            context.Response.Write("error");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}