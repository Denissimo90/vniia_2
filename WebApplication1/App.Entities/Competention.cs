using App.Entities.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities
{
    public class Competention
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public int CompetentionDtoId { get; set; }
        public CompetentionDto CompetentionDto { get; set; }

        public int GroupWorkplaceId { get;set; }
        public GroupWorkplace GroupWorkplace { get; set; }

        public List<Workplace> Workplaces { get; set; }
        public List<ApplicationUser> ApplicationUsers { get; set; }
        public List<Team> Teams { get; set; }
    }
}
