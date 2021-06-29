using System;
using System.ComponentModel.DataAnnotations;

namespace SakaryaBel.Web.Models
{
    public class Action
    {
        public int ActionId { get; set; }

        [Required]
        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Detay")]
        public string Description { get; set; }

    }

    public class ActionListModel
    {
        public int ActionId { get; set; }

        [Required]
        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Detay")]
        public string Description { get; set; }

        public int SubActionCount { get; set; }
        public int Number { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string StringCreatedDate { get; set; }
        public int SubActionType { get; set; }
    }
}