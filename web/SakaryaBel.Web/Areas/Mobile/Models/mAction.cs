using System;
using System.ComponentModel.DataAnnotations;

namespace SakaryaBel.Web.Areas.Mobile.Models
{
    public class mAction
    {
        public int ActionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int SubActionCount { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string StringCreatedDate { get; set; }
        public int SubActionType { get; set; }
    }

    public class mNewAction
    {
        public int ActionId { get; set; }

        [Required]
        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Detay")]
        public string Description { get; set; }
    }
}
