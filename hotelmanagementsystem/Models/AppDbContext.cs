using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace hotelmanagementsystem.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("AppDbContext") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }    
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Contact> Contacts { get; set; }

    }
}
