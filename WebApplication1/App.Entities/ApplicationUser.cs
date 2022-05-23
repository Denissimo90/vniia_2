using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Entities
{
    //[Index("Id", IsUnique = true, Name = "Так назвал")] Индекс
    //[Table("Users","atomdb")] //Назовёт таблицу не по имени калсса, а как указал
    public class ApplicationUser : IdentityUser
    {
        //[Column("user_id")] Переименовали колонку
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // для добавляения и измения в БД.
        //[Required] NOT NULL IsRequired
        public int Id { get; set; }
        //[NotMapped] Эта ассоциация не создать столбца в таблице.
        public string PwdSalt { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Username { get; set; }
        //[RegularExpression(@"/^(?!.*@.*@.*$)(?!.*@.*\-\-.*\..*$)(?!.*@.*\-\..*$)(?!.*@.*\-$)(.*@.+(\..{1,11})?)$/")]
        public string Email { get; set; }



    }
}
