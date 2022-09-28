namespace FialBP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Staff")]
    public partial class Staff
    {
        public int StaffId { get; set; }

        [StringLength(150)]
        public string StaffName { get; set; }

        [StringLength(50)]
        public string Gender { get; set; }

        public int? TitleID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? JoinDate { get; set; }

        [StringLength(150)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Age { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [Column(TypeName = "money")]
        public decimal? Salary { get; set; }

        [StringLength(500)]
        public string Picture { get; set; }

        public virtual Title Title { get; set; }
    }
}
