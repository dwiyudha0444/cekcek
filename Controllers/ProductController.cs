using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyNewApp3.Data;
using MyNewApp3.Models;
using System.Threading.Tasks;

namespace MyNewApp3.Controllers
{
    [Route("produk")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        
        [Route("")]
        [Route("daftar")] // Bisa diakses melalui /produk atau /produk/daftar
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        
    }
}
