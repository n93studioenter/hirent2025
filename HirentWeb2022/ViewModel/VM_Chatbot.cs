using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HirentWeb2022.ViewModel
{
    public class VM_Chatbot
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Fromtable { get; set; }
        public double Similarity { get; set; }  
    }
}