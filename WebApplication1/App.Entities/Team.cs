using App.Entities.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TeamDtoId { get; set; }
        //public TeamDto TeamDto { get; set; }
        public int? CompetentionId { get; set; }
        public Competention Competention { get; set; }
        //public List<ApplicationUser> ApplicationUsers { get; set; }
    }
}
