using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FialBP.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Bus> Buses { get; set; }
        public virtual DbSet<BusDT> BusDTs { get; set; }
        public virtual DbSet<BusType> BusTypes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Title> Titles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bus>()
                .Property(e => e.BusName)
                .IsUnicode(false);

            modelBuilder.Entity<Bus>()
                .Property(e => e.LicenseNo)
                .IsUnicode(false);

            modelBuilder.Entity<BusType>()
                .Property(e => e.BTName)
                .IsUnicode(false);

            modelBuilder.Entity<BusType>()
                .HasMany(e => e.Buses)
                .WithOptional(e => e.BusType1)
                .HasForeignKey(e => e.BusType);

            modelBuilder.Entity<Route>()
                .Property(e => e.RouteName)
                .IsUnicode(false);

            modelBuilder.Entity<Route>()
                .Property(e => e.StartPoint)
                .IsUnicode(false);

            modelBuilder.Entity<Route>()
                .Property(e => e.EndPoint)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.StaffName)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Age)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Salary)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Staff>()
                .Property(e => e.Picture)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserPass)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserType)
                .IsUnicode(false);

            modelBuilder.Entity<UserRole>()
                .Property(e => e.PageName)
                .IsUnicode(false);
        }
    }
}
