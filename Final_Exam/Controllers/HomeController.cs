using Final_Exam.DAL;
using Final_Exam.Models;
using Final_Exam.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Final_Exam.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult>  Index()
        {
            List<Product> products = await _context.Products.ToListAsync();


            HomeVM homeVM=new HomeVM
            {
                Products= products
            };

            return View(homeVM);
        }
    }
}
