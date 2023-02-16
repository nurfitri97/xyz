using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using XyzSystem.Data;
using XyzSystem.Models.Domain;
using XyzSystem.ViewModels;

namespace XyzSystem.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(int? categoryId)
        {


            IQueryable<Product> products = _context.Products;
            ProductListViewModel model = new ProductListViewModel();
            
            if (categoryId != null)
            {
                products = products.Where(p => p.Categories.Any(c => c.CategoryId == categoryId))
                                   .Include(x => x.Categories);
            }
            else
            {
                products = products.Include(x => x.Categories);
            }
            var allJobTagsList = await _context.Categories.ToListAsync();
            var a = allJobTagsList.Select(o => new SelectListItem
            {
                Text = o.CategoryName,
                Value = o.CategoryId.ToString()
            });
            model.products = products.ToList();
            ViewBag.AllCategories = a.ToList();
            return View();
            //return View(await products.ToListAsync());
            //IQueryable<Product> products = _context.Products;

            //if (categoryId != null)
            //{
            //    products = products.Where(p => p.Categories.Any(c => c.CategoryId == categoryId))
            //                       .Include(x => x.Categories);
            //}
            //else
            //{
            //    products = products.Include(x => x.Categories);
            //}

            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    products = products.Where(p => p.Description.Contains(searchString));
            //}

            //var productViewModels = new List<ProductViewModel>();
            //foreach (var product in products)
            //{
            //    var productViewModel = new ProductViewModel
            //    {
            //        Product = product,
            //        AllCategories = new SelectList(_context.Categories, "CategoryId", "CategoryName"),
            //        categoryId = categoryId // pass categoryId to the view
            //    };
            //    productViewModels.Add(productViewModel);
            //}

            //return View(productViewModels);
            //return View(await _context.Products.Include(x => x.Categories).ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductCode == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            var productViewModel = new ProductViewModel();

            var allJobTagsList = await _context.Categories.ToListAsync();
            productViewModel.AllCategories = allJobTagsList.Select(o => new SelectListItem
            {
                Text = o.CategoryName,
                Value = o.CategoryId.ToString()
            });
            return View(productViewModel);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SelectedCategories,Product")] ProductViewModel productViewModel)
        {
            productViewModel.Product.Categories ??= new();
            foreach (var categoryId in productViewModel.SelectedCategories ?? new())
            {
                productViewModel.Product.Categories.Add(await _context.Categories.SingleAsync(b => b.CategoryId == categoryId));
            }
            if (ModelState.IsValid)
            {
                _context.Add(productViewModel.Product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productViewModel);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var productViewModel = new ProductViewModel
            {
                Product = await _context.Products.Include(i => i.Categories).FirstAsync(i => i.ProductCode == id),
            };

            if (productViewModel.Product == null)
            {
                return NotFound();
            }

            var allJobTagsList = await _context.Categories.ToListAsync();
            productViewModel.AllCategories = allJobTagsList.Select(o => new SelectListItem
            {
                Text = o.CategoryName,
                Value = o.CategoryId.ToString()
            });

            return View(productViewModel);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SelectedCategories,Product")] ProductViewModel productViewModel)
        {
            if (id != productViewModel.Product.ProductCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                productViewModel.SelectedCategories ??= new();
                try
                {
                    _context.Update(productViewModel.Product);

                    _context.Entry(productViewModel.Product)
                        .Collection(b => b.Categories)
                        .Load();

                    productViewModel.Product.Categories ??= new();
                    productViewModel.Product.Categories.Clear();

                    await _context.SaveChangesAsync();

                    foreach (var categoryId in productViewModel.SelectedCategories)
                    {
                        productViewModel.Product.Categories.Add(await _context.Categories.SingleAsync(b => b.CategoryId == categoryId));
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(productViewModel.Product.ProductCode))
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
            return View(productViewModel);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductCode == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return _context.Products.Any(e => e.ProductCode == id);
        }
    }
}
