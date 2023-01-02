using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Código")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "¿Recordar este explorador?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Correo electrónico")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "¿Recordar cuenta?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        

        [Required(ErrorMessage ="El campo Correo Electrónico es Requerido")]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage ="El campo Contraseña es Requerida")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="El campo Nro Documento es Requerido")]
        [Display(Name ="Nro Documento:")]
        public long nroDocumento { get; set; }

        
        [Display(Name ="Tipo de Documento")]
        public string tipoDocumento { get; set; }

        [Required(ErrorMessage ="El campo Nombre Completo es Requerido")]
        [StringLength(60, ErrorMessage = "Su nombre no puede contener más de 60 letras")]
        [Display(Name ="Nombre")]
        public string nombre { get; set; }

        [Required]
        [StringLength(60,ErrorMessage ="Su apellido no puede contener más de 60 letras")]
        [Display(Name ="Apellido")]
        public string apellido { get; set; }

        
        [StringLength(50)]
        [Display(Name ="Sexo")]
        public string sexo { get; set; }

        [Required(ErrorMessage ="El campo Teléfono es Requerido")]
        [Display(Name ="Teléfono")]
        public long nroTelefono { get; set; }
        [Display(Name ="Código Area")]
        public int codigoAreaTelefono { get; set; }

        [Required]
        [Display(Name ="Ser contactado por publicante")]
        public bool permitirSerContactadoPublicante { get; set; }

        [Required]
        [Display(Name ="Ser notificado")]
        public bool permitirSerNotificado { get; set; }


    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }
    }
}
