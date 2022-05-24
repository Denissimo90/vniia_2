using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace App.Entities.Dto
{
    [Serializable]
    public class RoleDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("organizer")]
        public bool IsOrganizer { get; set; }

        public List<ParticipantDto> ParticipantDtos { get; set; }

    }
}
