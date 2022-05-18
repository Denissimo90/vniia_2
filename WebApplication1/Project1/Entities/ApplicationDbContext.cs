using Microsoft.EntityFrameworkCore;

namespace ReportApp.Entities
{
    public class ApplicationDbContext : DbContext
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
                    Id=1,
                    Email = "Vasya0Pupka@mail.ru",
                    FirstName = "Vasya",
                    LastName = "Pupkin",
                    MiddleName = "Vitlievich",
                    Password = "123456",
                    PwdSalt = "sal",
                    Username = "nagibator228"
                },

                new ApplicationUser
                {
                    Id=2,
                    Email = "killer@gmail.com",
                    FirstName = "Volodya",
                    LastName = "Putin",
                    MiddleName = "Vladimirivich",
                    Password = "ukrainIsMine",
                    PwdSalt = "gg",
                    Username = "VZPutin"
                },

                new ApplicationUser
                {
                    Id=3,
                    Email = "killer@gmail.com",
                    FirstName = "Vlad",
                    LastName = "Vladov",
                    MiddleName = "Vladimirivich",
                    Password = "12345",
                    PwdSalt = "hh",
                    Username = "Killer"
                },
                });
        }

        public DbSet<Manufacture> Manufactures { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductQty> ProductQties { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
