using System;
using System.ComponentModel.DataAnnotations;

namespace SakaryaBel.Web.Models
{
    public class SubAction
    {
        public int ActionId { get; set; }

        [Required]
        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Detay")]
        public string Description { get; set; }
        public int? Number { get; set; }

    }

    public class SubActionListModel
    {
        public int ActionId { get; set; }

        [Required]
        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Detay")]
        public string Description { get; set; }

        public int SubActionCount { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string StringCreatedDate { get; set; }

        public int? SubActionOfActionId { get; set; }
    }
}