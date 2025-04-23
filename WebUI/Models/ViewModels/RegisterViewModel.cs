using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "Ad alanı zorunludur")]
		[Display(Name = "Ad Soyad")]
		public string FullName { get; set; }

		[Required(ErrorMessage = "Email alanı zorunludur.")]
		[EmailAddress(ErrorMessage = "Geçerli bir email giriniz.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Şifre alanı zorunludur.")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Şifre tekrar zorunludur.")]
		[Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}
