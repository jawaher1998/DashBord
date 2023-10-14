using System.ComponentModel.DataAnnotations;

namespace DashBord.Models
{
	public class Products
	{
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }


    }
}