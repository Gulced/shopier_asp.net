using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Email alanı zorunludur.")]
		[EmailAddress(ErrorMessage = "Geçerli bir email giriniz.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Şifre alanı zorunludur.")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
