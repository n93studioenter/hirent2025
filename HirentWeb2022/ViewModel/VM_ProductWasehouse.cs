using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HirentWeb2022.Models;
namespace HirentWeb2022.ViewModel
{
	public class VM_ProductWasehouse
	{
		public tb_WareHouse tb_WareHouse { get; set; }
		public List<tb_Product> tb_Products { get; set; }
	}
}