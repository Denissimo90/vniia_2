using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReportApp.Entities
{
    [Serializable]
    public class Product
    {
        public Product()
        {
            Qties = new List<ProductQty>();
        }

        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("articul")]
        public string Articul { get; set; }
        [JsonPropertyName("caption")]
        public string Caption { get; set; }
        [JsonPropertyName("measure")]
        public string Measure { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        //[JsonIgnore]
        //public DateTime DateOfManufacture { get; set; }

        public int ManufactureId { get; set; }
        public Manufacture Manufacture { get; set; }

        public List<ProductQty> Qties { get; set; }

    }
}
