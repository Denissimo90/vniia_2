using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities
{
    public class GroupWorkplace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Workplace> Workplaces { get; set; }
        public List<Competention> Competents { get; set; }
    }
}
