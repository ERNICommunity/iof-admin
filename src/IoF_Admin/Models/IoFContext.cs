using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoF_Admin.Models
{
    public class IoFContext : DbContext
    {
        public DbSet<Aquarium> Aquariums { get; set; }
        public DbSet<Fish> Fishes { get; set; }
        public DbSet<Office> Offices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //TODO: Try new SqliteConnection($"Data Source={ApplicationData.Current.LocalFolder.Path}/test.db");
            /**
             *       string databaseFilePath = "SkillDB.db";
      try {
          databaseFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, databaseFilePath);
      } catch (InvalidOperationException) { }

      optionsBuilder.UseSqlite($"Data source={databaseFilePath}");
             * */

            optionsBuilder.UseSqlite("Filename=C:\\source\\iof-admin\\src\\IoF_Admin\\IoF.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
