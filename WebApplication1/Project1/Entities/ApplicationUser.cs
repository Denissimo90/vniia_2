using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReportApp.Entities
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string PwdSalt { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; } 
        public string Username { get; set; }
        //[RegularExpression(@"/^(?!.*@.*@.*$)(?!.*@.*\-\-.*\..*$)(?!.*@.*\-\..*$)(?!.*@.*\-$)(.*@.+(\..{1,11})?)$/")]
        public string Email { get; set; }
        [DefaultValue("false")]
        public bool IsAdmin { get; set; }


    }
}
