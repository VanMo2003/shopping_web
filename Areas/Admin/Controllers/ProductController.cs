using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using website_shopping.Models;
using website_shopping.Models.Contexts;

namespace website_shopping.Areas_Admin_Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ShopContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger, ShopContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var shopContext = _context.Products.Include(p => p.CategoryModel);
            return View(await shopContext.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products
                .Include(p => p.CategoryModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewData["id_category"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,UnitPrice,Quantity,id_category")] ProductModel productModel, [FromForm] IFormFile file)
        {
            ViewData["Image"] = null;

            if (file != null && ModelState.IsValid)
            {
                bool checkUpload = await UpdateLoadImage(file);
                if (checkUpload)
                {
                    productModel.ImageString = file.FileName;
                    _logger.LogInformation("image : " + productModel.ImageString);
                    productModel.PrintInfo();
                    _context.Add(productModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }
            if (ViewData["Image"] == null)
            {
                ViewData["Image"] = "Mời bạn chọn ảnh";
            }
            ViewData["id_category"] = new SelectList(_context.Categories, "Id", "Name", productModel.id_category);
            return View(productModel);

        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products.FindAsync(id);

            if (productModel == null)
            {
                return NotFound();
            }
            ViewData["id_category"] = new SelectList(_context.Categories, "Id", "Name", productModel.id_category);
            return View(productModel);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,UnitPrice,Quantity,id_category")] ProductModel productModel)
        {
            if (id != productModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductModelExists(productModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["id_category"] = new SelectList(_context.Categories, "Id", "Name", productModel.id_category);
            return View(productModel);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products
                .Include(p => p.CategoryModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productModel = await _context.Products.FindAsync(id);
            if (productModel != null)
            {
                _context.Products.Remove(productModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductModelExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        private async Task<bool> UpdateLoadImage(IFormFile file)
        {
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "products");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string fileName = Path.GetFileName(file.FileName);
            if (!CheckExistFileName(uploadsFolder, fileName))
            {
                string fileSavePath = Path.Combine(uploadsFolder, fileName);

                using FileStream stream = new(fileSavePath, FileMode.Create);
                await file.CopyToAsync(stream);
                return true;
            }
            else
            {
                ViewData["Image"] = "Ảnh đã tồn tại";
                return false;
            }

        }

        public bool CheckExistFileName(string uploadsFolder, string fileName)
        {
            DirectoryInfo directoryInfo = new(uploadsFolder); //Assuming Test is your Folder

            FileInfo[] Files = directoryInfo.GetFiles(); //Getting Text files

            foreach (FileInfo file in Files)
            {
                if (file.Name.Equals(fileName))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
