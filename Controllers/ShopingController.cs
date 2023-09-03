using DashBord.Models;
using DashBord.Views.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MailKit.Net.Smtp;

namespace DashBord.Controllers
{
	public class ShopingController : Controller
	{
        private readonly ApplicationDbContext _dbContext;
public ShopingController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
		{
            var proudect = _dbContext.Products.ToList();
			var proudectDetailes = _dbContext.ProductDetials.ToList();
			ViewBag.ProductDetials = proudectDetailes;

			return View(proudect);
		}
		public IActionResult ProductDetailes(int id)
		{
            var proudectDetailes = _dbContext.ProductDetials.Where(proudects => proudects.ProductId == id).ToList();
            var proudect = _dbContext.Products.ToList();
			ViewBag.ProductDetials = proudectDetailes;

            return View(proudect);
		}
		public async Task<string> sendemail()
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("testing", "gogoqoean3@gmail.com"));
			message.To.Add(MailboxAddress.Parse("goqoean4@gmail.com"));
			message.Subject = "test";
			message.Body = new TextPart("test")
			{
				Text = "<h1> this test message </h1>"
			};

			using (var clint = new SmtpClient())
			{
				try
				{
					clint.Connect("smtp.gmail.com", 587);
					clint.Authenticate("gogoqoean3@gmail.com", "jizxtsqdceauxspl");
					await clint.SendAsync(message);
					clint.Disconnect(true);

				}

				catch (Exception ex) {
				Console.WriteLine(ex.Message);
				}
			};
			return "ok";
		}
			

		

		[Authorize]
		public IActionResult chekout(int id)
		{
			var user = HttpContext.User.Identity.Name;
			var productDatails = _dbContext.ProductDetials.SingleOrDefault(p => p.ProductId == id);

			var prodect = _dbContext.Products.SingleOrDefault(p => p.Id == productDatails.ProductId);


			var	cart = new Cart()
				{
					IdCustomer = user,
					IdProducts = productDatails.ProductId,
					Color = productDatails.Color,
					Image = productDatails.Image,
					price = productDatails.Price,
					Qty = productDatails.Qty,
					Tax = 0.15,
					Total = productDatails.Price * (0.15) + productDatails.Price,

					ProductsName = prodect.ProductName


                };

        
				_dbContext.carts.Add(cart);
				_dbContext.SaveChanges();

			
			return View(cart);
		}
		public IActionResult Invoice()
		{
			return View();
		}


    }
}
//to send email 
//Install-Package MailKit
