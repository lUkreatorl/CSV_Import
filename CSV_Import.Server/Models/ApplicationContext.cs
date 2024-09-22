namespace CSV_Import.Server.Models
{
    using Microsoft.EntityFrameworkCore;
    using System;

    public class ApplicationContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>()
                .HasKey(p => p.Id);
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Persons { get; set; }
    }
}
