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
        
        
    }
}