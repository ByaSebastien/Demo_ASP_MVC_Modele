using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GUI
{
    public class Member
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Pseudo")]
        public string Pseudo { get; set; }
        [Required]
        [DisplayName("Adresse email")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class MemberForm
    {
        [Required]
        [DisplayName("Pseudo")]
        public string Pseudo { get; set; }
        [Required]
        [DisplayName("Adresse email")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }        
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Les deux mot de passe doivent correspondre")]
        public string PasswordRepeat { get; set; }
    }
    public class MemberConnection
    {
        [Required]
        [DisplayName("Pseudo")]
        public string Pseudo { get; set; }
        [Required]
        [DisplayName("Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class MemberSession
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Pseudo")]
        public string Pseudo { get; set; }
        [Required]
        [DisplayName("Adresse email")]
        public string Email { get; set; }
    }
    public class MemberDetail
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Pseudo")]
        public string Pseudo { get; set; }
        [Required]
        [DisplayName("Adresse email")]
        public string Email { get; set; }
        public IEnumerable<Game> Favorites { get; set; }
    }
}
