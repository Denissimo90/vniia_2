using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities
{
    public class Workplace
    {
        public int Id { get; set; }
        public string Designation { get; set; }


        public int GroupWorkplaceId { get; set; }
        public GroupWorkplace GroupWorkplace { get; set; }
        public int CompetentionId { get; set; } 
        public Competention Competention { get; set; }

    }
}
