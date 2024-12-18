using Microsoft.EntityFrameworkCore;

namespace Week12_CodeFirstBasic.Data
{
    public class PatikaFirstDbContext : DbContext   
    {
        public DbSet<Movie> Movies { get; set; }    

        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Postgresql için gerekli connection u yazdık
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=341;Database=PatikaCodeFirstDb1");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Game içindeki seed data ları yazdık

            modelBuilder.Entity<Game>().HasData(
                new Game() { Id = 1, Name = "Call Of Duty", Platform = "Bilgisayar", Rating = 8 },
                new Game() { Id = 2, Name = "Age Of Empires", Platform = "Bilgisayar", Rating = 6 },
                new Game() { Id = 3, Name = "Fruit Ninja", Platform = "Mobile", Rating = 4 },
                new Game() { Id = 4, Name = "Assasian Creed", Platform = "Xbox", Rating = 9 }
                );
            
            // Game içindeki seed data ları yazdık

            modelBuilder.Entity<Movie>().HasData(
                new Movie() { Id = 1, Title = "Kuzuların Sessizliği", Genre = "Korku", ReleaseYear = 2002 },
                new Movie() { Id = 2, Title = "Yüzüklerin Efendisi", Genre = "Aksiyon,BilimKurgu", ReleaseYear = 2000 },
                new Movie() { Id = 3, Title = "Dağ2", Genre = "Aksiyon", ReleaseYear = 2016 },
                new Movie() { Id = 4, Title = "Buz Devri", Genre = "Animasyon", ReleaseYear = 2005 }

              );


        }
    }
}
