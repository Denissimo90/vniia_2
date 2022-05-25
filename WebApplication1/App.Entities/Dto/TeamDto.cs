using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace App.Entities.Dto
{
    [Serializable]
    [Table("TeamDto")]
    public class TeamDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("competentionId")]
        public int CompetentionId { get; set; }
        /*public CompetentionDto Competention { get; set; }
        public List<ParticipantDto> Participants { get; set; }*/


    }
}
