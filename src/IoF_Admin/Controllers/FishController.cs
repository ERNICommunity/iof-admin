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
        public async Task<IActionResult> Details(int? id)
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
            FillDropdownData();

            return View();
        }
        
        // POST: Fish/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Fish fish)
        {
            FillDropdownData(fish);

            if (fish.AquariumId > 0)
            {
                var aquarium = _context.Aquariums.First(o => o.AquariumID == fish.AquariumId);
                if (aquarium != null)
                {
                    fish.Aquarium = aquarium;
                }

            }
            if(fish.OfficeId > 0)
            {
                var office = _context.Offices.First(o => o.OfficeID == fish.OfficeId);
                if (office != null)
                {
                    fish.Office = office;
                }
            }

            //We modified the model so we need to revalidate it.
            ModelState.Clear();
            
            if (TryValidateModel(fish) && ModelState.IsValid)
            {
                _context.Fishes.Add(fish);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fish);
        }

        // GET: Fish/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

            FillDropdownData(fish);

            return View(fish);
        }

        // POST: Fish/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Fish fish)
        {
            FillDropdownData(fish);

            if (fish.AquariumId > 0)
            {
                var aquarium = _context.Aquariums.First(o => o.AquariumID == fish.AquariumId);
                if (aquarium != null)
                {
                    fish.Aquarium = aquarium;
                }

            }
            if (fish.OfficeId > 0)
            {
                var office = _context.Offices.First(o => o.OfficeID == fish.OfficeId);
                if (office != null)
                {
                    fish.Office = office;
                }
            }

            //We modified the model so we need to revalidate it.
            ModelState.Clear();

            if (TryValidateModel(fish) && ModelState.IsValid)
            {
                _context.Update(fish);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fish);
        }

        // GET: Fish/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
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
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            Fish fish = await _context.Fishes.SingleAsync(m => m.FishID == id);
            _context.Fishes.Remove(fish);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private async void FillDropdownData(Fish fish = null)
        {            
            if (fish == null)
            {
                ViewBag.Offices = new SelectList(_context.Offices.ToList(), "OfficeID", "City");
                ViewBag.Aquariums = new SelectList(_context.Aquariums.ToList(), "AquariumID", "AquariumString");
            }
            else
            {
                ViewBag.Offices = new SelectList(_context.Offices.ToList(), "OfficeID", "City", fish.OfficeId);
                ViewBag.Aquariums = new SelectList(_context.Aquariums.ToList(), "AquariumID", "AquariumString", fish.AquariumId);
            }
        }
    }
}
