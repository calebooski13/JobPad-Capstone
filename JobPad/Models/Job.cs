using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPad.Models
{
    public class Job
    {
        public int Id { get; set; }
        public int EstimatedJobCost { get; set; }
        public int FinalCost { get; set; }
        public bool IsPaid { get; set; }
        public string Address { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public List<Material> Materials { get; set; } = new List<Material>();

    }
}
