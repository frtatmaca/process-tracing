
using SakaryaBel.Web.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace SakaryaBel.Web.Models
{
    public class PdfModel
    {
        public PdfModel()
        {
            actList = new List<ActivityListModel>();
        }

        public string UserName { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string CheifInfo { get; set; }
        public UserType UserType { get; set; }
        public string ProfilImageUrl { get; set; }
        public List<ActivityListModel> actList { get; set; }
    }

    public class ActivityListModel {
        public int ActivityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ActionId { get; set; }
        public ActivityStatus ActivityStatus { get; set; }
        public ActivityType ActivityType { get; set; }

        public string CreatedUserName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string StringCreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string StringCompletedDate { get; set; }
    }

    public class ContentDetail
    {
        public int SourceId { get; set; }
        public string SourceName { get; set; }
        public string SourceUrl { get; set; }
        public string SourceIcon { get; set; }
        //public List<Source> GroupSources { get; set; }
        public int SFType { get; set; }
        public int ConvertStatus { get; set; }
        public bool UserOnly { get; set; }
        public byte Available { get; set; }
        public int? GSourceId { get; set; }
        public int? GroupId { get; set; }
        public int? CourseId { get; set; }
        public int? FileId { get; set; }
    }

    public class StudentInformation
    {
        [Key]
        public Guid ID { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Course")]
        public string Course { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }
    }

    public class LinkModel
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string Icon { get; set; }
    }

    public enum ContentTypes
    {
        video = 1,
        Audio = 3,
        Image = 4,
        Pdf = 8,
        Zip = 9,
        Excell = 6,
        Word = 5,
        PowerPoint = 7
    }

    public class selectList
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }

    public class selectList2
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    public class Select2PagedResult
    {
        public int Total { get; set; }
        public List<Select2Result> Results { get; set; }
    }

    public class Select2Result
    {
        public string id { get; set; }
        public string text { get; set; }
    }
}
