using Microsoft.AspNetCore.Mvc;

namespace WebApi.Models.Entities
{
	public class Product
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int CreatedBy { get; set; } //Ürünü oluşturanın id'si.
		public DateTime CreatedAt { get; set; }

		public User Creator { get; set; } //Ürünü ekleyen rol değil kullanıcı.

		public byte[] Image { get; set; }
	}
}
