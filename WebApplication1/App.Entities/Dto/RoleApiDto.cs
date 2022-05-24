using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace App.Entities.Dto
{
    [Serializable]
    [Table("RoleDto")]
    public class RoleApiDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("organizer")]
        public bool IsOrganizer { get; set; }

        public List<ApplicationUser> ApplicationUsers { get; set; }

    }
}
