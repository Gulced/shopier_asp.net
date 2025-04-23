using Microsoft.AspNetCore.Mvc;

namespace WebApi.Models.Entities
{
	public class Order
	{
		public int OrderId { get; set; }
		public int UserId { get; set; } //Siparişi hangi kullanıcı verdi?
		public decimal TotalAmount { get; set; }
		public DateTime OrderDate { get; set; }
		public string Status { get; set; }

		public User User { get; set; }
		public ICollection<OrderItem> OrderItems { get; set; } //Bir sipariş birden fazla ürün içerebilir.
	}
}
