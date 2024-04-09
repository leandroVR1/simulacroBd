using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simulacromvc.Models;
using Simulacromvc.Data;

namespace Simulacromvc.Controllers{
    public class SectorsController : Controller{

        // Contexto de la base de datos
        public readonly CompanieContext _context;

        // Constructor que recibe el contexto de la base de datos
        public SectorsController(CompanieContext context)
        {
            _context = context;
        }

        // Método para mostrar la lista de sectores
        public async Task<IActionResult> Index(){
            var sectors = await _context.Sectors.ToListAsync();
            return View(sectors);
        }

        // Método para mostrar los detalles de un sector específico
        public async Task<IActionResult> Details(int? id){
            var sector = await _context.Sectors.FindAsync(id);
            return View(sector);
        }

        // Método para mostrar el formulario de creación de un nuevo sector
        public IActionResult Create(){
            return View();
        }

        // Método para procesar la creación de un nuevo sector
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

        // Método para mostrar el formulario de edición de un sector existente
        public async Task<IActionResult> Edit(int? id){
            var sector = await _context.Sectors.FindAsync(id);
            return View(sector);
        }

        // Método para procesar la edición de un sector existente
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

        // Método para eliminar un sector
        public async Task<IActionResult> Delete(int id, Sector sector)
        {
            _context.Sectors.Remove(sector);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Método para buscar sectores según un término de búsqueda
        public ActionResult Search(string searchTerm)
        {
            var sectors = _context.Sectors.ToList(); // Obtener todos los sectores

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var searchTermLower = searchTerm.ToLower();
                // Filtrar los sectores cuyo nombre contiene el término de búsqueda
                sectors = sectors.Where(u => u.Name.ToLower().Contains(searchTermLower)).ToList();
            }

            // Devolver vista parcial con lista de sectores
            return PartialView("_SectorList", sectors);
        }
    }
}
