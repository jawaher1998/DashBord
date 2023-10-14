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
          
			var proudectDetailes = _dbContext.ProductDetials.ToList();
			

			return View(proudectDetailes);
		}
		public IActionResult ProductDetailes(int id)
		{
            var proudectDetailes = _dbContext.ProductDetials.Where(proudects => proudects.Id == id).ToList();
            
			ViewBag.ProductDetials = proudectDetailes;

            var PriceCart = _dbContext.carts.ToList();
	

            return View(PriceCart);
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
		public IActionResult chekout(int id,int qty,double Price)
		{
		var user = HttpContext.User.Identity.Name;
			var productDatails = _dbContext.ProductDetials.SingleOrDefault(p => p.Id == id);
			
			var	cart = new Cart()
				{
					IdCustomer = user,
					Color= productDatails.Color,
					ProductsName= productDatails.ProductName,
					price= Price,
					Qty= qty,
					Image=productDatails.Image,

            };
                _dbContext.carts.Add(cart);
				_dbContext.SaveChanges();			
			return View(cart);
		}
		[HttpGet]
		public IActionResult chekout()
		{
			var cart = _dbContext.carts.ToList();
			return View(cart);
		}
		public IActionResult AddToCart(Cart cartinfo)
		{

			
            _dbContext.carts.Add(cartinfo);
            _dbContext.SaveChanges();
            return RedirectToAction("chekout");
        }



        /***	[HttpPost]
            public IActionResult updateitem(Cart cart)
            {
                var PriceCart = _dbContext.carts.ToList();
                ViewBag.Cart = PriceCart;

                Cart cartprice = _dbContext.carts.SingleOrDefault(p => p.Id == cart.Id) ?? new Cart();

                cartprice.price = cart.price;

                _dbContext.SaveChanges();


                return RedirectToAction("chekout");

            }
            public IActionResult plus(int id)
            {
                var cart = _dbContext.carts.FirstOrDefault(c => c.IdProducts == id);


                    cart.Qty += 1;


                _dbContext.SaveChanges();
                return RedirectToAction(nameof(ProductDetailes));
            } 
            public IActionResult minus(int id)
            {
                var cart = _dbContext.carts.FirstOrDefault(c => c.IdProducts == id);
                cart.Qty -= 1;
                _dbContext.SaveChanges();
                return RedirectToAction("ProductDetailes");
            }

            ***/




        public IActionResult Invoice(int id)
		{
            /**   var CartDatails = _dbContext.carts.SingleOrDefault(p => p.Id == id);

               var Invoice = new Invoice()
               {
                   CustomerId = CartDatails.IdCustomer,
                   ProudectId = CartDatails.IdProducts,
                   Price = CartDatails.price,




                    
               
   **/

            return View();
		}


    }
}
//to send email 
//Install-Package MailKit
