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
    [Table("CompetentionDto")]
    public class CompetentionDto
    {
        //[JsonIgnore]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("shortTitle")]
        public string ShortTitle { get; set; }
    }
}
