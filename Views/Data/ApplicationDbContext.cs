using DashBord.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DashBord.Views.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
		public DbSet<ProductDetials> ProductDetials { get; set; }
		public DbSet<Customer> customers { get; set; }
		public DbSet<Invoice> invoices { get; set; }
		public DbSet<Cart> carts { get; set; }
	}
}