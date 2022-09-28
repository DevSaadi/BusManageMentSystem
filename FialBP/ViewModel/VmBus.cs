using FialBP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FialBP.ViewModel
{
    public class VmBus
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public DateTime? BookingDate { get; set; }
        public string ImagePath { get; set; }
        public decimal Price { get; set; }
        public int BusDTId { get; set; }
        public string BusDTName { get; set; }
        public int Quantity { get; set; }
        public HttpPostedFileBase ImgFile { get; set; }
        public List<Category> CategoryList { get; set; }
    }
}