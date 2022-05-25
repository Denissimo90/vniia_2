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
            /*modelBuilder.HasSequence<int>("ApplicationUser");
            modelBuilder.Entity<ApplicationUser>()
                    .Property(o => o.Id)
                    .HasDefaultValueSql("NEXT VALUE FOR ApplicationUser");*/

            /*modelBuilder.Entity<ApplicationUser>()
            .HasOne(b => b.ParticipantDto)
            .WithOne(i => i.ApplicationUser)
            .HasForeignKey<ParticipantDto>(b => b.ApplicationUserForeignKey);*/

            /*modelBuilder.Entity<Participant>()
                .HasKey(c => new { c.ApplicationUser, c.ParticipantDto });*/
            /*modelBuilder.Entity<Competention>().HasData(
            new Competention[]
            {
                new Competention
                {
                    Id = -1,
                    Title = ""
                }
            });
            modelBuilder.Entity<ParticipantDto>().HasData(
            new ParticipantDto[]
            {
                new ParticipantDto
                {
                    Id = -1,
                }
            });*/
            modelBuilder.Entity<ApplicationUser>().HasData(
            new ApplicationUser[]
    {
                new ApplicationUser
                {
                    Email = "admin@mail.ru",
                    FirstName = "Vasya",
                    LastName = "Vasya",
                    MiddleName = "Vitlievich",
                    PasswordHash = "admin",
                    PwdSalt = "sal",
                    UserName = "admin",
                    BirthDate = new DateTime(1970,10,1),
                    EndDate = DateTime.Now,
                    BeginDate = new DateTime(DateTime.Now.Year - 2, DateTime.Now.Month, DateTime.Now.Day),
                    PersonId= 1,
                    PersonalNumber = "664363",
                    PlaceId = 1,
                    DepartmentCode="0035",
                    DepartmentId = 4/*,
                    CompetentionId = -1,
                    ParticipantDtoId = -1,
                    RoleDtoId = -1,
                    TeamId = -1*/
                },
    });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Manufacture> Manufactures { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductQty> ProductQties { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        public DbSet<TeamDto> TeamsDto { get; set; }
        public DbSet<ParticipantDto> ParticipantsDto { get; set; }
        public DbSet<RoleApiDto> RolesDto { get; set; }
        public DbSet<CompetentionDto> CompetentsDto { get; set; }
        public DbSet<Competention> Compitentions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Participant> Participants { get; set; }



    }
}
