using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using IoF_Admin.Models;
using IoF_Admin.Services;

namespace IoF_Admin.Controllers
{
    public class AquariumController : Controller
    {
        [FromServices]
        public IConfigurationService ConfigurationService { get; set; }

        private IoFContext _context;

        public AquariumController(IoFContext context)
        {
            _context = context;    
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
                return HttpNotFound();
            }

            Aquarium aquarium = await _context.Aquariums.Include(a => a.Office).Include(a => a.Fishes).SingleAsync(m => m.AquariumID == id);
            if (aquarium == null)
            {
                return HttpNotFound();
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
                return HttpNotFound();
            }

            Aquarium aquarium = await _context.Aquariums.SingleAsync(m => m.AquariumID == id);
            if (aquarium == null)
            {
                return HttpNotFound();
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
                return HttpNotFound();
            }

            Aquarium aquarium = await _context.Aquariums.SingleAsync(m => m.AquariumID == id);
            if (aquarium == null)
            {
                return HttpNotFound();
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
