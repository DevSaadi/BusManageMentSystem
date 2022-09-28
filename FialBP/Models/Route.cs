namespace FialBP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Route")]
    public partial class Route
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Route()
        {
            Schedules = new HashSet<Schedule>();
        }

        public int RouteId { get; set; }

        [StringLength(50)]
        public string RouteName { get; set; }

        [StringLength(50)]
        public string StartPoint { get; set; }

        [StringLength(50)]
        public string EndPoint { get; set; }

        public int? BusId { get; set; }

        public int? UnitPrice { get; set; }

        public virtual Bus Bus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
