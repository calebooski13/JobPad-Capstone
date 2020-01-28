using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPad.Models
{
    public class Material
    {
        public int Id { get; set; }
        public string MaterialType { get; set; }
        public string PaintBrand { get; set; }
        public string PaintColor { get; set; }
        public int TotalMaterialCost { get; set; }
        public Job Job { get; set; }
        public int JobId { get; set; }


    }
}
