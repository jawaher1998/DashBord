using DashBord.Views.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using Newtonsoft.Json.Linq;

namespace DashBord.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PhoneController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public JsonResult GetPhone()
        {
            var phoneData = _context.ProductDetials.ToList();
            if (phoneData == null)
            {
                return new JsonResult("no found!");
            }
            return new JsonResult(Ok(phoneData));
        }

        [HttpPost("getallphonew/{id}")]
        public JsonResult GetPhone(int id)
        {
            var phoneData = _context.ProductDetials.SingleOrDefault(p => p.Id == id);
            if (phoneData == null)
            {
                return new JsonResult("no found!");
            }
            return new JsonResult(Ok(phoneData.ProductName));
        }
        [HttpGet("cat")]
        public async Task<JsonResult> GetProduct()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://jsonplaceholder.typicode.com/posts");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
       /*     if (content != null)
            {
                foreach(var item in content)
                {
                    new JsonResult(item.ToString();
                }
            }*/


            return new JsonResult(content );
        }
    }
}
