using Final_Exam.Areas.Admin.ViewModels;
using Final_Exam.DAL;
using Final_Exam.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Final_Exam.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult>  Index()
        {
            List<Setting> settings = await _context.Settings.ToListAsync();

            return View(settings);
        }
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();

            Setting existed = await _context.Settings.FirstOrDefaultAsync(p => p.Id == id);

            if (existed == null) return NotFound();

            UpdateSettingVM settingVM= new UpdateSettingVM
            {
                Key=existed.Key, 
                Value=existed.Value
                
            };

            return View(settingVM);
        }
        [HttpPost]

        public async Task<IActionResult> Update(int id,UpdateSettingVM settingVM)
        {

            if (!ModelState.IsValid) return View(settingVM);

            if (id <= 0) return BadRequest();

            Setting existed = await _context.Settings.FirstOrDefaultAsync(p => p.Id == id);

            if (existed == null) return NotFound();


            bool result = await _context.Settings.AnyAsync(p => p.Key.Trim().ToLower() == settingVM.Key.Trim().ToLower() && p.Id != id);

            if (result)
            {
                ModelState.AddModelError("Key", "Bu adda Key mövcuddur");
                return View(settingVM);
            }

            existed.Value = settingVM.Value;
            existed.Key = settingVM.Key;


            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
