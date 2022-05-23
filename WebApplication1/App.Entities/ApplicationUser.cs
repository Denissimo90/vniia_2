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
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PersonalNumber { get; set; }
        public string PositionCode { get; set; }
        public string PositionName { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public int PlaceId { get; set; }
        public string PlaceCode { get; set; }
        public string PlaceName { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ShortName
        {
            get
            {
                return LastName + " " + FirstName.Substring(0, 1) + " " + MiddleName.Substring(0, 1);
            }
        }
        public string FullName
        {
            get
            {
                return LastName + " " + FirstName + " " + MiddleName;
            }
        }
        //[NotMapped] Эта ассоциация не создать столбца в таблице.
        public string PwdSalt { get; set; }
        //[RegularExpression(@"/^(?!.*@.*@.*$)(?!.*@.*\-\-.*\..*$)(?!.*@.*\-\..*$)(?!.*@.*\-$)(.*@.+(\..{1,11})?)$/")]
    }
}
