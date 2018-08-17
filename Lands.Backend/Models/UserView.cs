namespace Lands.Backend.Models
{
    using Domain;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    //Clase para manejar los usuarios con seguridad
    //como user esta conectada con EF, UserView tambien estaria connectada
    //para evitar eso usamos la annotation [NotMapped]
    [NotMapped]
    public class UserView: User
    {
        //UserView hereda los campos de user mas estos adicionales
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(20, ErrorMessage = "The length for field {0} must be betwen {1} and {2} characters", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The field {0} is required")]
        [Compare("Password", ErrorMessage = "The password and confirm does not match")]
        [Display(Name = "Password confirm")]
        public string PasswordConfirm { get; set; }
    }
}