using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace App.Entities.Dto
{
    [Serializable]
    public class TeamDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("competentionId")]
        public int CompetentionId { get; set; }
        [ForeignKey("CompetentionId")]
        public CompetentionDto Competention { get; set; }
        public int ActionId { get; set; }
        public ActionDto Action { get; set; }
        public List<ParticipantDto> Participants { get; set; }


    }
}
