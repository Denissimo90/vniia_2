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
        public RoleDto Role { get; set; }
        [JsonPropertyName("competentionId")]
        public int CompetentionId { get; set; }
        [ForeignKey("CompetentionId")]
        public CompetentionDto Competention { get; set; }
        public int ActionId { get; set; }
        public ActionDto Action { get; set; }
        public int TeamId { get; set; }
        [ForeignKey("TeamId")]
        public TeamDto Team { get; set; }

        public string ApplicationUserForeignKey { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
