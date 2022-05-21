﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using AuthService.Entities;

namespace AuthService.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220520061653_v1")]
    partial class v1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("AuthService.Entities.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("PwdSalt")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUsers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "Vasya0Pupka@mail.ru",
                            FirstName = "Vasya",
                            LastName = "Pupkin",
                            MiddleName = "Vitlievich",
                            Password = "123456",
                            PwdSalt = "sal",
                            Username = "nagibator228"
                        },
                        new
                        {
                            Id = 2,
                            Email = "killer@gmail.com",
                            FirstName = "Volodya",
                            LastName = "Putin",
                            MiddleName = "Vladimirivich",
                            Password = "ukrainIsMine",
                            PwdSalt = "gg",
                            Username = "VZPutin"
                        },
                        new
                        {
                            Id = 3,
                            Email = "killer@gmail.com",
                            FirstName = "Vlad",
                            LastName = "Vladov",
                            MiddleName = "Vladimirivich",
                            Password = "12345",
                            PwdSalt = "hh",
                            Username = "Killer"
                        });
                });

            modelBuilder.Entity("AuthService.Entities.Manufacture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Caption")
                        .HasColumnType("text");

                    b.Property<DateTime>("Entered")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("IpAddress")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Manufactures");
                });

            modelBuilder.Entity("AuthService.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Articul")
                        .HasColumnType("text");

                    b.Property<string>("Caption")
                        .HasColumnType("text");

                    b.Property<int>("ManufactureId")
                        .HasColumnType("integer");

                    b.Property<string>("Measure")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("ManufactureId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("AuthService.Entities.ProductQty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DateOfManufacture")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductQties");
                });

            modelBuilder.Entity("AuthService.Entities.Product", b =>
                {
                    b.HasOne("AuthService.Entities.Manufacture", "Manufacture")
                        .WithMany("Products")
                        .HasForeignKey("ManufactureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacture");
                });

            modelBuilder.Entity("AuthService.Entities.ProductQty", b =>
                {
                    b.HasOne("AuthService.Entities.Product", "Product")
                        .WithMany("Qties")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("AuthService.Entities.Manufacture", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("AuthService.Entities.Product", b =>
                {
                    b.Navigation("Qties");
                });
#pragma warning restore 612, 618
        }
    }
}
