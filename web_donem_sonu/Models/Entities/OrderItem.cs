using Microsoft.AspNetCore.Mvc;

namespace WebApi.Models.Entities
{
	public class OrderItem
	{
		public int OrderItemId { get; set; }
		public int OrderId { get; set; } //bu sipariş ögesi hangi siparişe ait.
		public int ProductId { get; set; } //bu öge hangi ürüne ait.
		public int Quantity { get; set; }
		public decimal UnitPrice { get; set; }

		public Order Order { get; set; }
		public Product Product { get; set; }
	}
}
