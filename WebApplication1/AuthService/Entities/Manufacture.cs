using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AuthService.Entities
{
    [Serializable]
    public class Manufacture
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("caption")]
        public string Caption { get; set; }
        
        [JsonIgnore]
        public DateTime Entered { get; set; }

        [JsonPropertyName("entered")]
        public string CustomEntered
        {
            set { Entered = DateTime.Parse(value); }
        }

        [JsonIgnore]
        public string IpAddress { get; set; }

        [JsonInclude]
        [JsonPropertyName("products")]
        public List<Product> Products { get; set; }
        public Manufacture()
        {
            Products = new List<Product>();
        }

    }

    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd hh:mm:ss";
        }
    }
}
