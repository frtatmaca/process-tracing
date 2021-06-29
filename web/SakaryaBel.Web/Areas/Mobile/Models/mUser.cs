
using SakaryaBel.Web.Enums;
using System.ComponentModel.DataAnnotations;
namespace SakaryaBel.Web.Areas.Mobile.Models
{
    public class mUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsOnline { get; set; }
        public int UserType { get; set; }
        public string ProfileImage { get; set; }
        public string Cheif { get; set; }
        public string Departman { get; set; }
        public string Status { get; set; }

        //public string[] ClassIds { get; set; }
        //public string[] CourseIds { get; set; }
        //public string[] ProgramIds { get; set; }
    }

    public class mChangePasswordViewModel
    {
        public string Id { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Şuanki Şifre")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Yeni Şifre Tekrar")]
        [Compare("NewPassword", ErrorMessage = "Yeni şifre ile eşleşmiyor")]
        public string ConfirmPassword { get; set; }
    }

    public class mRegister
    {
        [Required]
        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Soyad")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        public string ConfirmPassword { get; set; }

        public int UserType { get; set; }
        public string CheifId { get; set; }
    }

    public class mUserEdit
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Ad")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Soyad")]
        public string Surname { get; set; }

        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string ProfileImage { get; set; }

        public UserType UserType { get; set; }

        public string CheifId { get; set; }
        public string CheifName { get; set; }
        public ActiveStatus ActiveStatus { get; set; }
    }
}
