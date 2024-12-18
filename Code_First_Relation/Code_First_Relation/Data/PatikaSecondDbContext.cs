using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace Code_First_Relation.Data
{
    public class PatikaSecondDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=341;Database=PatikaCodeFirstDb2");
            //"Host=localhost;Port=5432;Username=postgres;Password=341;Database=PatikaCodeFirstDb1"
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)                   // her postun bir user' ait olduğunu belirtir.      
                .WithMany(u => u.Posts)                // bir user'ın birden fazla postu olabilir.
                .HasForeignKey(p => p.UserId);         // Post tablosundaki UserId sütununun, User tablosundaki Id sütunu ile ilişkilendirileceğini belirtir.  


                                                                 //Seed Data
            modelBuilder.Entity<User>().HasData(

                new User() { Id = 1, UserName = "Ahmet", Email = "Ahmet@hotmail.com", },
                new User() { Id = 2, UserName = "Mehmet", Email = "Mehmet@hotmail.com", },
                new User() { Id = 3, UserName = "Ayşe", Email = "Ayşe@hotmail.com", },
                new User() { Id = 4, UserName = "Fatma", Email = "Fatma@hotmail.com", }


                );                      

            modelBuilder.Entity<Post>().HasData(                // Seed  Data

              new Post() { Id = 1, Title = "Arabalar", Content = "Yolculuk", UserId = 1 },
              new Post() { Id = 2, Title = "Uçaklar", Content = "Emniyet", UserId = 2 },
              new Post() { Id = 3, Title = "Gemiler", Content = "Dayanıklılık", UserId = 3 },
              new Post() { Id = 4, Title = "Trenler", Content = "Konfor", UserId = 4 },
              new Post() { Id = 5, Title = "Otobüsler", Content = "Konfor", UserId = 4 }

              );

        }

    }
}
