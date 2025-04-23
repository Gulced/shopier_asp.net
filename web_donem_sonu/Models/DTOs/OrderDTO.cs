using Microsoft.AspNetCore.Mvc;

namespace WebApi.Models.DTOs
{
	public class OrderDTO
	{
		public int OrderId { get; set; }
		public decimal TotalAmount { get; set; }
		public DateTime OrderDate { get; set; }
		public string Status { get; set; }

		public List<OrderItemDTO> Items { get; set; }
	}

}
