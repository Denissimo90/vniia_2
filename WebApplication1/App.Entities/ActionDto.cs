using App.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities
{
    public class ActionDto
    {
        public string ActionId { get; set; }
        public string ActionName { get; set; }
        public List<ParticipantDto> ParticipantDtos { get; set; }
        public List<TeamDto> TeamDtos { get; set; }

    }
}
