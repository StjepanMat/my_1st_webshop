using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Webshop.Data;
using Webshop.Models;

namespace Webshop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminProductImageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminProductImageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminProductImage
        public async Task<IActionResult> Index(int? id)
        {
            if(id == null) return RedirectToAction("Index", "AdminProduct");
            ViewBag.ProductId = id;
            return View(await _context.ProductImage.Where(x=>x.ProductId==id).ToListAsync());
        }

        // GET: AdminProductImage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productImage == null)
            {
                return NotFound();
            }

            return View(productImage);
        }

        // GET: AdminProductImage/Create
        public IActionResult Create(int? id)
        {
            if (id == null) return RedirectToAction("index", "AdminProduct");
            if (_context.Product.Count(p => p.Id == id) == 0) return RedirectToAction("index", "AdminProduct");
            return View(new ProductImage() {ProductId=(int)id });
        }

        // POST: AdminProductImage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,IsMainImage,Title,FileName")] ProductImage productImage)
        {
            ModelState.Remove("ProductTitle");
            if (HttpContext.Request.Form.Files.Count > 0) ModelState.Remove("FileName");
            if (ModelState.IsValid)
            {
                var imageFile = HttpContext.Request.Form.Files.FirstOrDefault();
                var uploadPath = System.IO.Path.Combine("wwwroot", "images", "products", productImage.ProductId.ToString());
                if (!System.IO.Directory.Exists(uploadPath))
                {
                    System.IO.Directory.CreateDirectory(uploadPath);
                }
                if (imageFile != null)
                {
                    var fileName = System.IO.Path.Combine(uploadPath, imageFile.FileName);
                    using (var fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    fileName=fileName.Replace("wwwroot\\", "/").Replace("\\","/");
                    productImage.FileName = fileName;
                }
                productImage.Id = 0;
                _context.Add(productImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = productImage.ProductId });
            }
            return View(productImage);
        }

        // GET: AdminProductImage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            return RedirectToAction("Index", "AdminProduct");
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImage.FindAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }
            return View(productImage);
        }

        // POST: AdminProductImage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,IsMainImage,Title,FileName")] ProductImage productImage)
        {
            return RedirectToAction("Index", "AdminProduct");
            if (id != productImage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductImageExists(productImage.Id))
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
            return View(productImage);
        }

        // GET: AdminProductImage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productImage = await _context.ProductImage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productImage == null)
            {
                return NotFound();
            }

            return View(productImage);
        }

        // POST: AdminProductImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productImage = await _context.ProductImage.FindAsync(id);
            if (productImage != null)
            {
                var fileName = "wwwroot" + productImage.FileName.Replace("/", "\\");
                System.IO.File.Delete(fileName);
                _context.ProductImage.Remove(productImage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new {id=productImage.ProductId});
        }

        private bool ProductImageExists(int id)
        {
            return _context.ProductImage.Any(e => e.Id == id);
        }
    }
}
