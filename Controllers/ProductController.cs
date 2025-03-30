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

        // Menampilkan Form Create
        [HttpGet]
        [Route("tambah")] // URL: /produk/tambah
        public IActionResult Create()
        {
            return View();
        }

        // Menyimpan Data ke Database
        [HttpPost]
        [Route("tambah")]
        [ValidateAntiForgeryToken] // Mencegah serangan CSRF
        public async Task<IActionResult> Create([Bind("Name,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product); // Tambahkan ke database
                await _context.SaveChangesAsync(); // Simpan perubahan
                return RedirectToAction("Index"); // Kembali ke daftar produk
            }
            return View(product);
        }

        // Konfirmasi Hapus Produk
        [HttpGet]
        [Route("hapus/{id}")] // URL: /produk/hapus/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product); // Menampilkan konfirmasi penghapusan
        }

        // Proses Hapus Produk
        [HttpPost, ActionName("Delete")]
        [Route("hapus/{id}")]
        [ValidateAntiForgeryToken] // Mencegah serangan CSRF
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product); // Hapus produk dari database
                await _context.SaveChangesAsync(); // Simpan perubahan
            }
            return RedirectToAction("Index"); // Kembali ke daftar produk
        }

         // GET: Product/Edit/5
        [HttpGet]
        [Route("Product/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        [HttpPost]
        [Route("Product/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Price")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(product);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

    }
}
