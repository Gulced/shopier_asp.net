using Microsoft.AspNetCore.Mvc;

namespace WebApi.Models.Entities
{
	public class User 
	{
		public int UserId { get; set; } //primary key
		public string FullName { get; set; }
		public string Email { get; set; }
		public string PasswordHash { get; set; }
		public int RoleId { get; set; } //Role tablosuna foreign key.Admin mi user mı?

		public Role Role { get; set; } //RoleId foreign keyini ilişkilendirdik.
		public ICollection<Order> Orders { get; set; } //Bir kullanıcının birden fazla siparişi olabilir.

	}
}
