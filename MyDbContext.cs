using Microsoft.EntityFrameworkCore;

namespace QuasarWebApi
{
    public class MyDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
         => options.UseMySQL("server=localhost;database=quasarapp;user=root;password=Qaz@1234;port=3306;");

        public DbSet<Tram> Trams { get; set; }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tram>().HasKey(t => t.MaTram);
            modelBuilder.Entity<Tram>().ToTable("Tram");
        }
    }
}
