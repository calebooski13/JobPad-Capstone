using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobPad.Models
{
    public class Material
    {
        public int Id { get; set; }
        [Display(Name = "Miscellaneous")]
        public string MaterialType { get; set; }
        [Display(Name = "Paint Brand")]
        public string PaintBrand { get; set; }
        [Display(Name = "Paint Color")]
        public string PaintColor { get; set; }
        [Display(Name = "Total Material Cost")]
        public int TotalMaterialCost { get; set; }
        public Job Job { get; set; }
        public int JobId { get; set; }


    }
}
