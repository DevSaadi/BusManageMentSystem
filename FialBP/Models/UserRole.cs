namespace FialBP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserRole")]
    public partial class UserRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserRoleID { get; set; }

        public int? UserID { get; set; }

        [StringLength(50)]
        public string PageName { get; set; }

        public bool? IsCreate { get; set; }

        public bool? IsRead { get; set; }

        public bool? IsUpdate { get; set; }

        public bool? IsDelete { get; set; }
    }
}
