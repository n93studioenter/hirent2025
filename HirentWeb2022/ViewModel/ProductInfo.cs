using HirentWeb2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HirentWeb2022.ViewModel
{
    public class ProductVM
    {
        public tb_Product tb_Product { get; set; }
        public tb_Product_Translation tb_Product_Translation { get; set; } 
        public tb_ProductCategorySelection tb_ProductCategorySelection { get; set; }
        public tb_WareHouse tb_WareHouse { get; set; } 
    }

    public class ProductInfo
    {
        public tb_Product_Translation tb_Product_Translation { get; set; }
        public long ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductAvatar { get; set; }
        public string ProductCity { get; set; }
        public string ProductDistrict { get; set; }
        public string ProductWard { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string ProductSpecification { get; set; }
        public string ProductInstruction { get; set; }
        public Nullable<int> StatusPercentage { get; set; }
        public Nullable<byte> StatusAvailability { get; set; }
        public string PricePerBlock { get; set; }
        public string PricePerDay { get; set; }
        public string PricePerMonth { get; set; }
        public Nullable<long> ProductMainCate { get; set; }
        public Nullable<long> ProductSubCate1 { get; set; }
        public Nullable<long> ProductSubCate2 { get; set; }
        public Nullable<long> ProductSubCate3 { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<double> ProductValue { get; set; }
        public string ListBy1 { get; set; }
        public string ListBy2 { get; set; }
        public string ListBy3 { get; set; }
        public string ListBy4 { get; set; }
        public string ListBy5 { get; set; }
        public string ListBy6 { get; set; }
        public string ListBy7 { get; set; }
        public string YoutubeLink { get; set; }
        public string Language { get; set; }
        public Nullable<int> Position { get; set; }
        public string Makho { get; set; }
        public Nullable<int> Hinhthucdatcoc { get; set; }
        public Nullable<int> Cocbangtienmat { get; set; }
        public Nullable<int> Cocbanggiayto { get; set; }
        public Nullable<int> Cocbanggiaytovahopdong { get; set; }
        public Nullable<double> Cocbanggiaytovahopdongprice { get; set; }
        public Nullable<int> Cocbangtaisan { get; set; }
        public Nullable<int> Soluongton { get; set; }
        //Em
        public string ProductName_EN { get; set; }
        public string ShortDescription_EN { get; set; }
        public string FullDescription_EN { get; set; }
        public string PricePerBlock_EN { get; set; }
        public string PricePerDay_EN { get; set; }
        public string PricePerMonth_EN { get; set; }
        public string ProductSpecification_EN { get; set; }
        public string ProductInstruction_EN { get; set; }

        public List<srtsub> lstsub { get; set; } = new List<srtsub>();
        public int WarehouseId { get; set; }
        //
        public string strPriceByBlock { get; set; }
        public string strPriceByDay { get; set; }
        public string strPriceByMonth { get; set; }
        //
        public float CostInstallation { get; set; }
        public float CostDelivery { get; set; }
        public float CostInstallAndDelivery { get; set; }
        public string Tulapdat { get; set; }
        public string Huongdansudung { get; set; }
        public string Thotoilap { get; set; }
        public string Thotoithao { get; set; }
        public string Thodenlapvathao { get; set; }
        public string Tuvanchuyen { get; set; }
        public string Giaotannha { get; set; }
        public string Nhantratannha { get; set; }
        public string Giaovanhantratannha { get; set; }
        //en
        public float CostInstallationEN  { get; set; }
        public float CostDeliveryEN { get; set; }
        public float CostInstallAndDeliveryEN { get; set; }
        public string TulapdatEN { get; set; }
        public string HuongdansudungEN { get; set; }
        public string ThotoilapEN { get; set; }
        public string ThotoithaoEN { get; set; }
        public string ThodenlapvathaoEN { get; set; }
        public string TuvanchuyenEN { get; set; }
        public string GiaotannhaEN { get; set; }
        public string NhantratannhaEN { get; set; }
        public string GiaovanhantratannhaEN { get; set; }
        public List<objsub> lstProductAccessorySelection { get; set; }
        public string WarehouseName { get; set; }
    }
    public class objsub
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class srtsub
    {
        public int id { get; set; }
        public objsub submain { get; set; }
        public objsub sub1 { get; set; }
        public objsub sub2 { get; set; }
    }
    public class AccessorySelection
    {
        public int id { get; set; }
        public int name { get; set; }
    }
}