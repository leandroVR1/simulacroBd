using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Simulacromvc.Models;
using Simulacromvc.Data;


namespace Simulacromvc.Controllers{
    public class SectorsController : Controller{
        public readonly CompanieContext _context;
        public SectorsController(CompanieContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(){
            var sectors = await _context.Sectors.ToListAsync();
            return View(sectors);

        }
        public async Task<IActionResult> Details(int? id){
            var sector = await _context.Sectors.FindAsync(id);
            return View(sector);
        }
        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sector sector){
            if(ModelState.IsValid){
                _context.Sectors.Add(sector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sector);
          
        }
        public async Task<IActionResult> Edit(int? id){
            var sector = await _context.Sectors.FindAsync(id);
            return View(sector);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Sector sector){
            if(ModelState.IsValid){
                _context.Sectors.Update(sector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sector);
        }
        public async Task<IActionResult> Delete(int id, Sector sector)
        {
            _context.Sectors.Remove(sector);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        
        }
        public ActionResult Search(string searchTerm)
{
    var sectors = _context.Sectors.ToList(); // Obtener todos los sectores

    if (!string.IsNullOrEmpty(searchTerm))
    {
        
        var searchTermLower = searchTerm.ToLower();
        sectors = sectors.Where(u => u.Name.ToLower().Contains(searchTermLower)).ToList();
    }

    return PartialView("_SectorList", sectors); // Devolver vista parcial con lista de sectores
}





}
}
