﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IoF_Admin.Models
{
    public class IoFContext : DbContext
    {
        public DbSet<Aquarium> Aquariums { get; set; }
        public DbSet<Fish> Fishes { get; set; }
        public DbSet<Office> Offices { get; set; }

        public IoFContext(DbContextOptions<IoFContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
