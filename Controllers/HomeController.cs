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

        [Authorize]
		public IActionResult Index()
        {
            //var name = HttpContext.User.Identity.Name;
            //  CookieOptions cookieOptions = new CookieOptions();
            //   cookieOptions.Expires = DateTime.Now.AddMinutes(10);
            //    Response.Cookies.Append("Name", name, cookieOptions);

            //HttpContext.Session.SetString("Name", name);
            //TempData["Name"] = "إضافة منتج";
			ViewBag.Name = "إضافة منتج";
			var proudect = _dbContext.Products.ToList();

              

			return View(proudect);
        }


        public IActionResult Privacy()
        {
            return View();
        }

		public IActionResult ProductDetails()   
		{
            //if we use cookes i will get data like thes
            // ViewBag.Name = Request.Cookies["Name"];

            //ViewBag.Name = HttpContext.Session.GetString("Name");
            //ViewBag.Name = TempData["Name"];
            ViewBag.Name = " تفاصيل المنتج";
			var proudect = _dbContext.Products.ToList();
            var proudectDetailes = _dbContext.ProductDetials.ToList();
			ViewBag.ProductDetials = proudectDetailes;
			return View(proudect);
		}
        [HttpPost]
		public IActionResult CreateNewProduct(Products products)
		{

			_dbContext.Products.Add(products);
			_dbContext.SaveChanges();
			TempData["alter"] = "تم إضافة منتج جديد";

			return RedirectToAction("index");
		}

	public IActionResult AddProductDetails(ProductDetials productDetails)
        { 
		
            _dbContext.ProductDetials.Add(productDetails);
            _dbContext.SaveChanges();
			return RedirectToAction("ProductDetails");
		}
        [HttpPost]
       public IActionResult ProductDetails(int id)
        {
			var proudectDetailes = _dbContext.ProductDetials.Where(proudects=> proudects.ProductId == id).ToList();

			var proudect = _dbContext.Products.ToList();
            ViewBag.ProductDetials = proudectDetailes;

			return View(proudect);
        }
        public IActionResult Delete(int id)
        {
            var productid = _dbContext.Products.SingleOrDefault(p => p.Id == id);
            if (productid != null)
            {
                _dbContext.Products.Remove(productid);
                _dbContext.SaveChanges();
				TempData["alter"] = "تم حذف المنتج";

			}
			return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
			ViewBag.Name = " تعديل المنتجات";

			var productid = _dbContext.Products.SingleOrDefault(p => p.Id == id);
			return View(productid);
        }
        public IActionResult Update(Products products) {
            Products product = _dbContext.Products.SingleOrDefault(p => p.Id == products.Id) ?? new Products();

                product.ProductName = products.ProductName;
            
            _dbContext.SaveChanges();
            TempData["alter"] = "تم تعديل البيانات";

			return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Index(string productName)
        {

            var ProductName = _dbContext.Products.Where(PN => PN.ProductName.Contains(productName)).ToList();

            TempData["show"] = "إظهار الكل";

            return View(ProductName);
        }
		public IActionResult DeleteProductDetail(int id)
		{
			var productid = _dbContext.ProductDetials.SingleOrDefault(p => p.ProductId == id);
			if (productid != null)
			{
				_dbContext.ProductDetials.Remove(productid);
				_dbContext.SaveChanges();
				TempData["alter"] = "تم حذف المنتج";

			}
			return RedirectToAction("ProductDetails");
		}
        public IActionResult EditProducDetailes(int id)
        {
			var productid = _dbContext.ProductDetials.SingleOrDefault(p => p.Id == id);

			return View(productid);
        }
		public IActionResult UpdateDetails(ProductDetials products)
		{
			ProductDetials product = _dbContext.ProductDetials.SingleOrDefault(p => p.Id == products.Id) ?? new ProductDetials();

			product.Description = products.Description;
            product.Price = products.Price;
            product.Qty= products.Qty;
            product.Color = products.Color;
            product.Model = product.Model;
            product.Image = products.Image;

			_dbContext.SaveChanges();
			TempData["alter"] = "تم تعديل البيانات";

			return RedirectToAction("ProductDetails");
		}
        public IActionResult PayementAccept()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PayementAccept(PaymentAccept paymentAccept)
        {
            if(ModelState.IsValid)
            {
                return RedirectToAction("index");
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}