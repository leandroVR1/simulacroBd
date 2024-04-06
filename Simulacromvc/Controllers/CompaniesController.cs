using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simulacromvc.Controllers;
using Simulacromvc.Models;
using Simulacromvc.Data;

namespace Simulacromvc.Controllers{
    public class CompaniesController : Controller{
        public readonly CompanieContext _context;
        public CompaniesController(CompanieContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(){
            var companies = await _context.Companies.ToListAsync();
            return View(companies);
        }
        public async Task<IActionResult> Details(int? id){
            
            return View(await _context.Companies.FirstOrDefaultAsync(x => x.Id == id));
        }
        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Companie companie){
            if(ModelState.IsValid){
                _context.Companies.Add(companie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(companie);
        }
        public async Task<IActionResult> Edit(int? id){
            return View(await _context.Companies.FirstOrDefaultAsync(x =>x.Id == id));

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Companie companie){
            if(ModelState.IsValid){
                _context.Companies.Update(companie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(companie);
        }
        public async Task<IActionResult> Delete(int id, Companie companie)
        {
        
            _context.Companies.Remove(companie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
           public ActionResult Search(string searchTerm)
{
    var companies = _context.Companies.ToList(); // Obtener todos los sectores

    if (!string.IsNullOrEmpty(searchTerm))
    {
        
        var searchTermLower = searchTerm.ToLower();
        companies = companies.Where(u => u.Name.ToLower().Contains(searchTermLower)).ToList();
    }

    return PartialView("_CompanieList", companies); // Devolver vista parcial con lista de sectores
}
        
        
    }
}