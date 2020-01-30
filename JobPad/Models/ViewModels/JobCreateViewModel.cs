using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobPad.Models.ViewModels
{
    public class JobCreateViewModel
    {
        public Job Job { get; set; }
        public List<SelectListItem> CustomerOptions { get; set; }
    }
}

