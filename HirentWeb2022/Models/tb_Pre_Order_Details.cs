
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace HirentWeb2022.Models
{

using System;
    using System.Collections.Generic;
    
public partial class tb_Pre_Order_Details
{

    public int pOrderDetailId { get; set; }

    public Nullable<int> pOrderId { get; set; }

    public Nullable<int> rentalType { get; set; }

    public Nullable<int> productQty { get; set; }

    public Nullable<System.DateTime> timePickup { get; set; }

    public Nullable<System.DateTime> timeReturn { get; set; }

    public Nullable<int> serviceId { get; set; }

    public Nullable<int> depositId { get; set; }

    public Nullable<int> deliveryId { get; set; }

    public Nullable<int> deliveryAddress { get; set; }

    public string deliveryNote { get; set; }

    public Nullable<int> paymentMethodId { get; set; }

    public Nullable<System.DateTime> createTimeClone { get; set; }

    public string OrderCode { get; set; }

    public Nullable<double> Totals { get; set; }

    public Nullable<double> TotalInstall { get; set; }

    public Nullable<double> TotalDelivery { get; set; }

    public string Dichvuvanchuyen { get; set; }

    public string Dichvulapdat { get; set; }

    public Nullable<double> Totaldeposit { get; set; }

    public Nullable<int> hinhthuc { get; set; }

    public string hinhthuccontent { get; set; }

    public string GhichuDichVu { get; set; }

    public string GhiChuVanChuyen { get; set; }

    public string Ghichucoc1 { get; set; }

    public string Ghichucoc2 { get; set; }

    public string Imagegc1 { get; set; }

    public string Imagegc2 { get; set; }

}

}
