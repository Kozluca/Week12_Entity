using Microsoft.EntityFrameworkCore;
using Survivor.Model.Entities;

namespace Survivor.Data.Migrations
{
    public class SurvivorDbContext : DbContext
    {
        public SurvivorDbContext(DbContextOptions<SurvivorDbContext> options) : base(options)
        {

        }
        public DbSet<Competitor> Competitors { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=341;Database=Survivor");
        }

      
        // Seed data (with UTC times)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships
            modelBuilder.Entity<Competitor>()
                .HasOne(y => y.Category)
                .WithMany(c => c.Competitors)
                .HasForeignKey(y => y.CategoryId);

            // Seed data (with UTC times)
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    CreatedDate = new DateTime(2024, 10, 10),
                    ModifiedDate = new DateTime(2024, 10, 10),
                    IsDeleted = false,
                    Name = "Ünlüler"
                },
                new Category
                {
                    Id = 2,
                    CreatedDate = new DateTime(2024, 10, 10),
                    ModifiedDate = new DateTime(2024, 10, 10),
                    IsDeleted = false,
                    Name = "Gönüllüler"
                }
            );


            //modelBuilder.Entity<Category>().HasData(
            //    new Category() { Id = 1, CreatedDate = new DateTime(2024, 10, 10), ModifiedDate = new DateTime(2024, 10, 10), IsDeleted = false, Name = "Ünlüler" },
            //    new Category() { Id = 2, CreatedDate = new DateTime(2024, 10, 10), ModifiedDate = new DateTime(2024, 10, 10), IsDeleted = false, Name = "Gönüllüler" }

            //    );


            modelBuilder.Entity<Competitor>().HasData(

                new Competitor() { Id = 1, CreatedDate = new DateTime(2024, 10, 10), ModifiedDate = new DateTime(2024, 10, 10), IsDeleted = false, FirstName = "Acun", LastName = "Ilıcalı", CategoryId = 1 },
                new Competitor() { Id = 2, CreatedDate = new DateTime(2024, 10, 10), ModifiedDate = new DateTime(2024, 10, 10), IsDeleted = false, FirstName = "Aleyna", LastName = "Avcı", CategoryId = 1 },
                new Competitor() { Id = 3, CreatedDate = new DateTime(2024, 10, 10), ModifiedDate = new DateTime(2024, 10, 10), IsDeleted = false, FirstName = "Hadise", LastName = "AçıkGöz", CategoryId = 1 },
                new Competitor() { Id = 4, CreatedDate = new DateTime(2024, 10, 10), ModifiedDate = new DateTime(2024, 10, 10), IsDeleted = false, FirstName = "Sertan", LastName = "Bozkuş", CategoryId = 1 },
                new Competitor() { Id = 5, CreatedDate = new DateTime(2024, 10, 10), ModifiedDate = new DateTime(2024, 10, 10), IsDeleted = false, FirstName = "Özge", LastName = "Açık", CategoryId = 1 },
                new Competitor() { Id = 6, CreatedDate = new DateTime(2024, 10, 10), ModifiedDate = new DateTime(2024, 10, 10), IsDeleted = false, FirstName = "Kıvanç", LastName = "Tatlıtuğ", CategoryId = 1 },
                new Competitor() { Id = 7, CreatedDate = new DateTime(2024, 10, 10), ModifiedDate = new DateTime(2024, 10, 10), IsDeleted = false, FirstName = "Ahmet", LastName = "Yılmaz", CategoryId = 2 },
                new Competitor() { Id = 8, CreatedDate = new DateTime(2024, 10, 10), ModifiedDate = new DateTime(2024, 10, 10), IsDeleted = false, FirstName = "Elif", LastName = "Demirtaş", CategoryId = 2 },
                new Competitor() { Id = 9, CreatedDate = new DateTime(2024, 10, 10), ModifiedDate = new DateTime(2024, 10, 10), IsDeleted = false, FirstName = "Cem", LastName = "Öztürk", CategoryId = 2 },
                new Competitor() { Id = 10, CreatedDate = new DateTime(2024, 10, 10), ModifiedDate = new DateTime(2024, 10, 10), IsDeleted = false, FirstName = "Ayşe", LastName = "Karaca", CategoryId = 2 }

                );
        }
    }
}

