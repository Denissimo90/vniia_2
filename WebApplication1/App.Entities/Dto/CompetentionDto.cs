using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace App.Entities.Dto
{
    [Serializable]
    public class CompetentionDto
    {
        //[JsonIgnore]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("shortTitle")]
        public string ShortTitle { get; set; }

        public List<ParticipantDto> ParticipantDtos { get; set; }
        public List<TeamDto> TeamDtos { get; set; }
    }
}
