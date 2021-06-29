﻿using SakaryaBel.Web.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SakaryaBel.Web.Models
{
    public class Problem
    {
        public int? ProblemId { get; set; }

        [Required]
        [Display(Name = "Ad")]
        public string Name { get; set; }
        
        [Display(Name = "Detay")]
        public string Description { get; set; }        

        public string FileUrl { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int? FileId { get; set; }

    }

    public class ProblemDetailListModel
    {
        public ProblemDetailListModel()
        {
            messages = new List<MessageDetailListModel>();
        }

        public int ActionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string BackGroundColor { get; set; }
        public string FileName { get; set; }
        public FileType? FileType { get; set; }
        public string FileUrl { get; set; }
        public int? ProblemOfActId { get; set; }
        public string ActId { get; set; }
        public string ActionName { get; set; }
        public string CreatedUserId { get; set; }

        public List<MessageDetailListModel> messages { get; set; }

        //public int SubActionCount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string StringCreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string StringCompletedDate { get; set; }
    }

    public class MessageDetailListModel
    {
        public int MessageId { get; set; }
        public string Body { get; set; }
        public string SenderUserId { get; set; }
        public string SenderUserName { get; set; }
        public string SenderProfilImage { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string StringCreatedDate { get; set; }
    }

    public class ProblemListModel
    {
        public int ActionId { get; set; }

        [Required]
        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Detay")]
        public string Description { get; set; }
        public string BackGroundColor { get; set; }

        //public int SubActionCount { get; set; }
        public string CreatedUserName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string StringCreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string StringCompletedDate { get; set; }
    }
}