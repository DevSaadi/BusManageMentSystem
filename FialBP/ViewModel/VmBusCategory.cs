using FialBP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FialBP.ViewModel
{
    public class VmBusCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<Category> CategoryList { get; set; }
        public List<VmBus> BusList { get; set; }
    }
}