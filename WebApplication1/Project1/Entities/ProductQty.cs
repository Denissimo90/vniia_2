using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReportApp.Entities
{
    [Serializable]
    public class ProductQty
    {
        public int Id { get; set; }
        [JsonPropertyName("qty")]
        public int Amount { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
