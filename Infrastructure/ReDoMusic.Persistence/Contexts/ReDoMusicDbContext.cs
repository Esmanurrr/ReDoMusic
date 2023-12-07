using Microsoft.EntityFrameworkCore;
using ReDoMusic.Core.Domain.Common;
using ReDoMusic.Core.Domain.Entities;
using ReDoMusic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReDoMusic.Infrastructure.Persistence.Contexts
{
    public class ReDoMusicDbContext : DbContext
    {
        public ReDoMusicDbContext(DbContextOptions options) : base(options)
        {
        }

        public ReDoMusicDbContext()
        {
        }

        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> ShoppingCartItems { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configurations.GetString("ConnectionStrings:PostgreSQL"));
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instrument>().HasKey(i => i.Id);
            modelBuilder.Entity<Instrument>()
                .HasOne(x => x.Category);
                
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                    ((ICreatedOn)entry.Entity).CreatedOn = DateTime.UtcNow;

                if (entry.State == EntityState.Modified)
                    ((IModifiedOn)entry.Entity).ModifiedOn = DateTime.UtcNow;

            }

            return base.SaveChanges();
        }

    }
}
