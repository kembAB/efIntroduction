using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCWebApp.Models.Person.ViewModels
{
    public class SortOptionsViewModel
    {
        [Display(Name = "Reverse Alphabetical Order")]
        public bool ReverseAplhabeticalOrder { get; set; }
    }
}
