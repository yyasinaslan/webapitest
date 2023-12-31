﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Entities;

namespace Web.Context
{
    public class MyDbContext : IdentityDbContext<User>
    {
        public MyDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=ef;Username=postgres;Password=123456");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //var admin = new User
            //{
            //    UserName = "admin",
            //    Email = "admin@example.com"
            //};


            base.OnModelCreating(builder);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var insertedEntities = ChangeTracker.Entries<BaseEntity>().Where(entry => entry.State == EntityState.Added)
                .ToList();

            insertedEntities.ForEach(e =>
            {
                e.Entity.CreatedDate = DateTime.Now;
                e.Entity.UpdatedDate = DateTime.Now;
            });

            var updatedEntities = ChangeTracker.Entries<BaseEntity>().Where(entry => entry.State == EntityState.Modified)
                .ToList();

            updatedEntities.ForEach(e => { e.Entity.UpdatedDate = DateTime.Now; });


            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}