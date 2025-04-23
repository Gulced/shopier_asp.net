using Microsoft.AspNetCore.Mvc;

namespace WebApi.Models.Entities
{
	public class Role
	{
		public int RoleId { get; set; } //primary key
		public string RoleName { get; set; }
		public ICollection<User> Users { get; set; } //Bir role birden fazla kullanıcıya ait olabilir.
	}
}
