using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GUI
{
    public class Game
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [DisplayName("Nom")]
        public string Name { get; set; }
        [DisplayName("Description")]
        public string? Description { get; set; }
        [DisplayName("Nombre de joueur minimum")]
        public int Nb_Player_Min { get; set; }
        [DisplayName("Nombre de joueur maximum")]
        public int Nb_Player_Max { get; set; }
        [DisplayName("Age")]
        public int? Age { get; set; }
        [DisplayName("Coop")]
        public bool IsCoop { get; set; }
        public string Image { get; set; }

    }
    public class GameForm
    {
        [Required(ErrorMessage = "Le nom est obligatoire")]
        [DisplayName("Nom")]
        public string Name { get; set; }
        [DisplayName("Description")]
        [MaxLength(200,ErrorMessage = "La description peut faire maximum 200 caractère")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Nombre de joueur minimum obligatoire")]
        [DisplayName("Nombre de joueur minimum")]
        [Range(1,int.MaxValue,ErrorMessage = "Nombre de joueur minimum doit être superieur à 0")]
        public int Nb_Player_Min { get; set; }
        [Required(ErrorMessage = "Nombre de joueur maximum obligatoire")]
        [DisplayName("Nombre de joueur maximum")]
        [Range(1, double.MaxValue, ErrorMessage = "Nombre de joueur maximum doit être superieur à 0")]
        public int Nb_Player_Max { get; set; }
        [DisplayName("Age")]
        [Range(0,int.MaxValue,ErrorMessage = "L' Age doit etre positif")]
        public int? Age { get; set; }
        [DisplayName("Coop")]
        public bool IsCoop { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
