using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FialBP.ViewModel
{
    public class VmEp
    {
        public int StaffId { get; set; }

        public string StaffName { get; set; }

        public string Gender { get; set; }

        public int? TitleID { get; set; }
        public string TitleName { get; set; }

        public DateTime? JoinDate { get; set; }


        public string Address { get; set; }


        public string Age { get; set; }


        public string Email { get; set; }


        public decimal? Salary { get; set; }


        public string Picture { get; set; }
    }
}