using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using IoF_Admin.Models;

namespace IoF_Admin.Controllers
{
    public class AquariaController : Controller
    {
        private IoFContext _context;

        public AquariaController(IoFContext context)
        {
            _context = context;    
        }

        // GET: Aquaria
        public async Task<IActionResult> Index()
        {
            var ioFContext = _context.Aquariums.Include(a => a.Office);
            return View(await ioFContext.ToListAsync());
        }

        // GET: Aquaria/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Aquaria/Create
        public IActionResult Create()
        {
            ViewData["OfficeId"] = new SelectList(_context.Offices, "OfficeID", "Office");
            return View();
        }

        // POST: Aquaria/Create
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
            ViewData["OfficeId"] = new SelectList(_context.Offices, "OfficeID", "Office", aquarium.OfficeId);
            return View(aquarium);
        }

        // GET: Aquaria/Edit/5
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
            ViewData["OfficeId"] = new SelectList(_context.Offices, "OfficeID", "Office", aquarium.OfficeId);
            return View(aquarium);
        }

        // POST: Aquaria/Edit/5
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
            ViewData["OfficeId"] = new SelectList(_context.Offices, "OfficeID", "Office", aquarium.OfficeId);
            return View(aquarium);
        }

        // GET: Aquaria/Delete/5
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

        // POST: Aquaria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Aquarium aquarium = await _context.Aquariums.SingleAsync(m => m.AquariumID == id);
            _context.Aquariums.Remove(aquarium);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
