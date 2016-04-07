using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using IoF_Admin.Models;

namespace IoF_Admin.Controllers
{
    public class FishController : Controller
    {
        private IoFContext _context;

        public FishController(IoFContext context)
        {
            _context = context;    
        }

        // GET: Fish
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fishes.ToListAsync());
        }

        // GET: Fish/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Fish fish = await _context.Fishes.SingleAsync(m => m.FishID == id);
            if (fish == null)
            {
                return HttpNotFound();
            }

            return View(fish);
        }

        // GET: Fish/Create
        public IActionResult Create()
        {
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
                return RedirectToAction("Index");
            }
            return View(fish);
        }

        // GET: Fish/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Fish fish = await _context.Fishes.SingleAsync(m => m.FishID == id);
            if (fish == null)
            {
                return HttpNotFound();
            }
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
                return RedirectToAction("Index");
            }
            return View(fish);
        }

        // GET: Fish/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Fish fish = await _context.Fishes.SingleAsync(m => m.FishID == id);
            if (fish == null)
            {
                return HttpNotFound();
            }

            return View(fish);
        }

        // POST: Fish/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            Fish fish = await _context.Fishes.SingleAsync(m => m.FishID == id);
            _context.Fishes.Remove(fish);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
