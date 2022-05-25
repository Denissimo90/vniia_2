using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace App.Entities.Dto
{
    [Serializable] 
    [Table("ParticipantDto")]
    public class ParticipantDto
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("secondName")]
        public string SecondName { get; set; }
        [JsonPropertyName("thirdName")]
        public string ThirdName { get; set; }
        [JsonPropertyName("roleId")]
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public RoleApiDto RoleDto { get; set; }
        [JsonPropertyName("competentionId")]
        public int CompetentionId { get; set; }
        public CompetentionDto CompetentionDto { get; set; }

        public List<ApplicationUser> ApplicationUsers { get; set; }
    }
}
