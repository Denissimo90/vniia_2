using App.Entities.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities
{
    public class Participant
    {
        public int Id { get; set; }
        /*public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int CompetentionId { get; set; }
        public Competention Competention { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }*/
    }
}
