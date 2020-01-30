using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPad.Models
{
    public class Job
    {
        public int Id { get; set; }
        [Display(Name = "Job Estimate")]
        public int EstimatedJobCost { get; set; }
        [Display(Name = "Total")]
        public int FinalCost { get; set; }
        [Display(Name = "Paid")]
        public bool IsPaid { get; set; }
        public string Address { get; set; }
        [Display(Name = "Customer ")]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public List<Material> Materials { get; set; } = new List<Material>();

    }
}
