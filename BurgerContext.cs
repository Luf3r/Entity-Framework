using System.Reflection;
using Microsoft.EntityFrameworkCore;


namespace Prokerka 
{
    public class BurgerContext : DbContext
    {
        public DbSet<Burger> Burgers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("FileName=BurgerDB", option =>
            {
                option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Burger>().ToTable("Burgers", "test");
            modelBuilder.Entity<Burger>(entity =>
            {
                entity.HasKey(k => k.BurgerId);
                entity.HasIndex(i => i.BurgerName).IsUnique();
            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}