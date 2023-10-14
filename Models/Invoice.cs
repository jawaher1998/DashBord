using System.ComponentModel.DataAnnotations;

namespace DashBord.Models
{
	public class Invoice
	{
		[Key]
		public int Id { get; set; }
		public int CustomerId { get; set; }
		public int ProudectId { get; set; }
		public decimal Price { get; set; }
		public string QTY { get; set; }
		public decimal Tax { get; set; }
		public decimal Discount { get; set; }
		public double Total { get; set; }
	}
}
