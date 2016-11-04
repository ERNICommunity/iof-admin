using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IoF_Admin.Models;
using IoF_Admin.Services;

namespace IoF_Admin.Controllers
{
    public class FishController : Controller
    {
        private IoFContext _context;
        private readonly IConfigurationService _configService;

        public FishController(IoFContext context, IConfigurationService configService)
        {
            _context = context;
            _configService = configService;
        }

        // GET: Fish
        public async Task<IActionResult> Index()
        {
            var ioFContext = _context.Fishes.Include(f => f.Aquarium).Include(f => f.Office);
            return View(await ioFContext.ToListAsync());
        }

        // GET: Fish/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Fish fish = await _context.Fishes.Include(f => f.Aquarium).Include(f => f.Office).SingleAsync(m => m.FishID == id);
            if (fish == null)
            {
                return NotFound();
            }

            return View(fish);
        }

        // GET: Fish/Create
        public IActionResult Create()
        {
            FillDropdownData();

            return View();
        }
        
        // POST: Fish/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Fish fish)
        {            
            if (ModelState.IsValid)
            {
                _context.Fishes.Add(fish);
                await _context.SaveChangesAsync();
                _configService.PublishConfiguration(fish.AquariumID);
                return RedirectToAction("Index");
            }
            FillDropdownData(fish);
            return View(fish);
        }

        // GET: Fish/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Fish fish = await _context.Fishes.SingleAsync(m => m.FishID == id);
            if (fish == null)
            {
                return NotFound();
            }

            FillDropdownData(fish);
            return View(fish);
        }

        // POST: Fish/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Fish fish)
        {
            if (ModelState.IsValid)
            {
                _context.Update(fish);
                await _context.SaveChangesAsync();
                _configService.PublishConfiguration(fish.AquariumID);
                return RedirectToAction("Index");
            }

            FillDropdownData(fish);
                      
            return View(fish);
        }

        // GET: Fish/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Fish fish = await _context.Fishes.SingleAsync(m => m.FishID == id);
            if (fish == null)
            {
                return NotFound();
            }

            return View(fish);
        }

        // POST: Fish/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Fish fish = await _context.Fishes.SingleAsync(m => m.FishID == id);
            _context.Fishes.Remove(fish);
            await _context.SaveChangesAsync();
            _configService.PublishConfiguration(fish.AquariumID);
            return RedirectToAction("Index");
        }

        private void FillDropdownData(Fish fish = null)
        {            
            if (fish == null)
            {
                ViewBag.OfficeID = new SelectList(_context.Offices.ToList(), "OfficeID", "City");
                ViewBag.AquariumID = new SelectList(_context.Aquariums.ToList(), "AquariumID", "AquariumString");
            }
            else
            {
                ViewBag.OfficeID = new SelectList(_context.Offices.ToList(), "OfficeID", "City", fish.OfficeID);
                ViewBag.AquariumID = new SelectList(_context.Aquariums.ToList(), "AquariumID", "AquariumString", fish.AquariumID);
            }
        }
    }
}
