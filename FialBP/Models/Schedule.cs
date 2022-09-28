namespace FialBP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Schedule")]
    public partial class Schedule
    {
        public int ScheduleId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DepartureTime { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ArrivalTime { get; set; }

        public int? BusId { get; set; }

        public int? RouteId { get; set; }

        public virtual Bus Bus { get; set; }

        public virtual Route Route { get; set; }
    }
}
