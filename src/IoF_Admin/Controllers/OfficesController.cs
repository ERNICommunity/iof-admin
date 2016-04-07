using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using IoF_Admin.Models;

namespace IoF_Admin.Controllers
{
    public class OfficesController : Controller
    {
        private IoFContext _context;

        public OfficesController(IoFContext context)
        {
            _context = context;    
        }

        // GET: Offices
        public async Task<IActionResult> Index()
        {
            return View(await _context.Offices.ToListAsync());
        }

        // GET: Offices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Office office = await _context.Offices.SingleAsync(m => m.OfficeID == id);
            if (office == null)
            {
                return HttpNotFound();
            }

            return View(office);
        }

        // GET: Offices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Offices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Office office)
        {
            if (ModelState.IsValid)
            {
                _context.Offices.Add(office);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(office);
        }

        // GET: Offices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Office office = await _context.Offices.SingleAsync(m => m.OfficeID == id);
            if (office == null)
            {
                return HttpNotFound();
            }
            return View(office);
        }

        // POST: Offices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Office office)
        {
            if (ModelState.IsValid)
            {
                _context.Update(office);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(office);
        }

        // GET: Offices/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Office office = await _context.Offices.SingleAsync(m => m.OfficeID == id);
            if (office == null)
            {
                return HttpNotFound();
            }

            return View(office);
        }

        // POST: Offices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            Office office = await _context.Offices.SingleAsync(m => m.OfficeID == id);
            _context.Offices.Remove(office);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
