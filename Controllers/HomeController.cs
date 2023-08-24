using DashBord.Models;
using DashBord.Views.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DashBord.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _dbContext;



		public HomeController(ApplicationDbContext applicationDbContext)
        {
            this._dbContext = applicationDbContext;

        }
        
		public IActionResult Index()
        {

            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

		public IActionResult ProductDetails()   
		{
			var proudect = _dbContext.Products.ToList();
            var proudectDetiales =_dbContext.ProductDetials.ToList();
            ViewBag.ProudectDetials = proudectDetiales;
			return View(proudect);
		}
        [HttpPost]
		public IActionResult CreateNewProduct(Products products)
		{

			_dbContext.Products.Add(products);
			_dbContext.SaveChanges();
			return RedirectToAction("index");
		}
	/*	public IActionResult AddProductDetails(ProductDetials productDetails)
        { 
		
            _dbContext.ProductDetials.Add(productDetails);
            _dbContext.SaveChanges();
			return RedirectToAction("index");
		}*/
     /*   public IActionResult ProudectDetailes(int id)
        {
			var proudectDetailes = _dbContext.ProductDetials.Where(proudects=> proudects.ProductId == id).ToList();

			var proudect = _dbContext.Products.ToList();
            ViewBag.pro=proudectDetailes;

			return View(proudect);
        }
     */

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}