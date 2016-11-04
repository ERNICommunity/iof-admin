using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IoF_Admin.Models;
using IoF_Admin.Services;


namespace IoF_Admin.Controllers
{
    public class AquariumController : Controller
    {
        private IoFContext _context;
        private readonly IConfigurationService _configService;

        public AquariumController(IoFContext context, IConfigurationService configService)
        {
            _context = context;
            _configService = configService; 
        }

        // GET: Aquarium
        public async Task<IActionResult> Index()
        {
            var ioFContext = _context.Aquariums.Include(a => a.Office);
            return View(await ioFContext.ToListAsync());
        }

        // GET: Aquarium/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Aquarium aquarium = await _context.Aquariums.Include(a => a.Office).Include(a => a.Fishes).SingleAsync(m => m.AquariumID == id);
            if (aquarium == null)
            {
                return NotFound();
            }

            return View(aquarium);
        }

        // GET: Aquarium/Create
        public IActionResult Create()
        {
            FillDropdownData();
            return View();
        }

        // POST: Aquarium/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Aquarium aquarium)
        {
            if (ModelState.IsValid)
            {
                _context.Aquariums.Add(aquarium);
                await _context.SaveChangesAsync();
                _configService.PublishConfiguration(aquarium.HardwareID);
                return RedirectToAction("Index");
            }
            FillDropdownData(aquarium);
            return View(aquarium);
        }

        // GET: Aquarium/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Aquarium aquarium = await _context.Aquariums.SingleAsync(m => m.AquariumID == id);
            if (aquarium == null)
            {
                return NotFound();
            }
            FillDropdownData(aquarium);
            return View(aquarium);
        }

        // POST: Aquarium/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Aquarium aquarium)
        {
            if (ModelState.IsValid)
            {
                _context.Update(aquarium);
                await _context.SaveChangesAsync();
                _configService.PublishConfiguration(aquarium.HardwareID);
                return RedirectToAction("Index");
            }
            FillDropdownData(aquarium);
            return View(aquarium);
        }

        // GET: Aquarium/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Aquarium aquarium = await _context.Aquariums.SingleAsync(m => m.AquariumID == id);
            if (aquarium == null)
            {
                return NotFound();
            }

            return View(aquarium);
        }

        // POST: Aquarium/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Aquarium aquarium = await _context.Aquariums.SingleAsync(m => m.AquariumID == id);
            _context.Aquariums.Remove(aquarium);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private void FillDropdownData(Aquarium aquarium = null)
        {
            if (aquarium == null)
            {
                ViewBag.OfficeID = new SelectList(_context.Offices.ToList(), "OfficeID", "City");

            }
            else
            {
                ViewBag.OfficeID = new SelectList(_context.Offices.ToList(), "OfficeID", "City", aquarium.OfficeID);
            }
        }
    }
}
