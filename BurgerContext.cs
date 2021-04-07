using System.Reflection;
using Microsoft.EntityFrameworkCore;


namespace Prokerka 
{
    public class BurgerContext : DbContext
    {
        public DbSet<Burger> Burgers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Using Reflection, we'll get the name of assembly, that executing our code 
            optionsBuilder.UseSqlite("FileName=BurgerDB", option =>
            {
                option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Creating Table Burgers with schema Prokerka
            modelBuilder.Entity<Burger>().ToTable("Burgers", "Prokerka");
            // Makes ID primary key and burger's name unique
            modelBuilder.Entity<Burger>(entity =>
            {
                entity.HasKey(k => k.BurgerId);
                entity.HasIndex(i => i.Name).IsUnique();
            });

            //Creates module
            base.OnModelCreating(modelBuilder);
        }
    }
}
