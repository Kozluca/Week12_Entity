﻿// <auto-generated />
using Code_First_Relation.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Code_First_Relation.Data.Migrations
{
    [DbContext(typeof(PatikaSecondDbContext))]
    partial class PatikaSecondDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Code_First_Relation.Data.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "Yolculuk",
                            Title = "Arabalar",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Content = "Emniyet",
                            Title = "Uçaklar",
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            Content = "Dayanıklılık",
                            Title = "Gemiler",
                            UserId = 3
                        },
                        new
                        {
                            Id = 4,
                            Content = "Konfor",
                            Title = "Trenler",
                            UserId = 4
                        },
                        new
                        {
                            Id = 5,
                            Content = "Konfor",
                            Title = "Otobüsler",
                            UserId = 4
                        });
                });

            modelBuilder.Entity("Code_First_Relation.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "Ahmet@hotmail.com",
                            UserName = "Ahmet"
                        },
                        new
                        {
                            Id = 2,
                            Email = "Mehmet@hotmail.com",
                            UserName = "Mehmet"
                        },
                        new
                        {
                            Id = 3,
                            Email = "Ayşe@hotmail.com",
                            UserName = "Ayşe"
                        },
                        new
                        {
                            Id = 4,
                            Email = "Fatma@hotmail.com",
                            UserName = "Fatma"
                        });
                });

            modelBuilder.Entity("Code_First_Relation.Data.Post", b =>
                {
                    b.HasOne("Code_First_Relation.Data.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Code_First_Relation.Data.User", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
