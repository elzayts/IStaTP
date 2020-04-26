using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace istatp_lab2.Models
{
    public class Lab2Context:DbContext
    {
      
        public virtual DbSet<ClientRoom> ClientRoom { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<Orders> Orders{ get; set; }
        public virtual DbSet<Fines> Fines { get; set; }
    

        public Lab2Context(DbContextOptions<Lab2Context> options): base(options)
        {
            Database.EnsureCreated();
        }

    }
}
