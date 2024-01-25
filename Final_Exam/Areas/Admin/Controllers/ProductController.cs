using Final_Exam.Areas.Admin.ViewModels;
using Final_Exam.DAL;
using Final_Exam.Models;
using Final_Exam.Utilities.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Final_Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products= await _context.Products.ToListAsync();

            return View(products);
        }
        public IActionResult Create()
        {
           

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM productVM)
        {
           
            if(!ModelState.IsValid) return View(productVM);

            bool result= await _context.Products.AnyAsync(p=>p.Name== productVM.Name);

            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda name mövcuddur");
                return View(productVM);
            }

            if (!productVM.Photo.ValidatorType("image/"))
            {
                ModelState.AddModelError("Photo", "The shape type is not suitable");
                return View(productVM);

            }

            if (!productVM.Photo.ValidatorSize(3*1024))
            {
                ModelState.AddModelError("Photo", "The size of the image should not be more than 3mb");
                return View(productVM);

            }

            string image = await productVM.Photo.CreateFile(_env.WebRootPath, "assets", "images");

            Product product = new Product
            {
                Name = productVM.Name,
                Catagory = productVM.Catagory,
                ImageUrl = image
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();

            Product existed =await _context.Products.FirstOrDefaultAsync(p=> p.Id == id);

            if (existed == null) return NotFound();


            UpdateProductVM productVM = new UpdateProductVM
            {
                Catagory= existed.Catagory,
                ImageUrl= existed.ImageUrl,
                Name= existed.Name

            };
            

            return View(productVM);
        }

        [HttpPost]

        public async Task<IActionResult> Update(int id,UpdateProductVM productVM)
        {
            if (!ModelState.IsValid) return View(productVM);

            if (id <= 0) return BadRequest();

            Product existed = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (existed == null) return NotFound();


            bool result = await _context.Products.AnyAsync(p => p.Name == productVM.Name && p.Id!=id);

            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda name mövcuddur");
                return View(productVM);
            }

            if(productVM.Photo is not null)
            {

                if (!productVM.Photo.ValidatorType("image/"))
                {
                    ModelState.AddModelError("Photo", "The shape type is not suitable");
                    return View(productVM);

                }

                if (!productVM.Photo.ValidatorSize(3 * 1024))
                {
                    ModelState.AddModelError("Photo", "The size of the image should not be more than 3mb");
                    return View(productVM);

                }

                string image = await productVM.Photo.CreateFile(_env.WebRootPath, "assets", "images");

                existed.ImageUrl.DeleteFile(_env.WebRootPath, "assets", "images");
                existed.ImageUrl= image;

            }
            existed.Name = productVM.Name;
            existed.Catagory= productVM.Catagory;

            await _context.SaveChangesAsync();


            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();

            Product existed = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (existed == null) return NotFound();


             _context.Remove(existed);

            existed.ImageUrl.DeleteFile(_env.WebRootPath, "assets", "images");

            await _context.SaveChangesAsync();


            return RedirectToAction("Index");
        }

    }
}
