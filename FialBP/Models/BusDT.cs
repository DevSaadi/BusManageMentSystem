namespace FialBP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BusDT")]
    public partial class BusDT
    {
        public int BusDTId { get; set; }

        public string BusDTName { get; set; }

        public decimal Price { get; set; }

        public string ImagePath { get; set; }

        public DateTime? BookingDate { get; set; }

        public int Quantity { get; set; }

        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
