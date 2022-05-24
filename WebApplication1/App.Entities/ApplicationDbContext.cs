using App.Entities.Dto;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
namespace App.Entities
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Tutorial
            /*
             * Тутор по методам в FluentAPI. Кое-что описано в аннтоции у полей ApplicationUser
             */

            // переименовали колонку
            //modelBuilder.Entity<ApplicationUser>().Property(u => u.Id).HasColumnName("user_id");

            // primary key
            //modelBuilder.Entity<ApplicationUser>().HasKey(u => u.Id);         

            //составной ключ
            //modelBuilder.Entity<ApplicationUser>().HasKey(u => new { u.Id, u.Username });

            //альтернативный ключ. Делает поле NOT NULL.
            //На уровне базы данных это выражается в установке для соответствующих столбцов ограничения на уникальность.
            //modelBuilder.Entity<ApplicationUser>().HasAlternateKey(u => new { u.Id, u.Username });

            //Индексы. IsUnique гарантирует, что в бд значение будет уникальным.
            //modelBuilder.Entity<ApplicationUser>().HasIndex(u => u.Id).IsUnique().HasDatabaseName("IndexNameInDB");


            //Вот так можно выполнить суквел скрипт при добавлении в базу 
            /*modelBuilder.Entity<User>()
            .Property(u => u.CreatedAt)
            .HasDefaultValueSql("GETDATE()");*/

            //Заполняет вычисляемый стоблец
            /*modelBuilder.Entity<User>()
                .Property(u => u.FullName)
                .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");*/
            #endregion
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().HasData(
            new ApplicationUser[]
    {
                new ApplicationUser
                {
                    Email = "Vasya0Pupka@mail.ru",
                    FirstName = "Vasya",
                    LastName = "Pupkin",
                    MiddleName = "Vitlievich",
                    PasswordHash = "123456",
                    PwdSalt = "sal",
                    UserName = "nagibator228",
                    BirthDate = new DateTime(1970,10,1),
                    EndDate = DateTime.Now,
                    BeginDate = new DateTime(DateTime.Now.Year - 2, DateTime.Now.Month, DateTime.Now.Day),
                    PersonId= 1,
                    PersonalNumber = "664363",
                    PlaceId = 1,
                    DepartmentCode="0035",
                    DepartmentId = 4,
                },

                new ApplicationUser
                {
                    Email = "killer@gmail.com",
                    FirstName = "Volodya",
                    LastName = "Putin",
                    MiddleName = "Vladimirivich",
                    PasswordHash = "ukrainIsMine",
                    PwdSalt = "gg",
                    UserName = "VZPutin",
                    BirthDate = new DateTime(1960,10,1),
                    EndDate = DateTime.Now,
                    BeginDate = new DateTime(DateTime.Now.Year - 10, DateTime.Now.Month, DateTime.Now.Day),
                    PersonId= 2,
                    PersonalNumber = "44325",
                },

                new ApplicationUser
                {
                    Email = "killer@gmail.com",
                    FirstName = "Vlad",
                    LastName = "Vladov",
                    MiddleName = "Vladimirivich",
                    PasswordHash = "12345",
                    PwdSalt = "hh",
                    UserName = "Killer",
                    BirthDate = new DateTime(1980,10,1),
                    EndDate = DateTime.Now,
                    BeginDate = new DateTime(DateTime.Now.Year - 5, DateTime.Now.Month, DateTime.Now.Day),
                    PersonId= 3,
                    PersonalNumber = "1999",
                },
    });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Manufacture> Manufactures { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductQty> ProductQties { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<TeamDto> Teams { get; set; }
        public DbSet<ParticipantDto> Participants { get; set; }
        public DbSet<RoleDto> RolesDto { get; set; }
        public DbSet<CompetentionDto> Competents { get; set; }
        public DbSet<ActionDto> Actions { get; set; }

    }
}
