using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Simulacromvc.Controllers;
using Simulacromvc.Models;
using Simulacromvc.Data;
using Simulacromvc.Helpers;
using Simulacromvc.Providers;

namespace Simulacromvc.Controllers{
    public class CompaniesController : Controller{
        public readonly CompanieContext _context;
        private readonly HelperUploadFiles helperUploadFiles;

        public CompaniesController(CompanieContext context, HelperUploadFiles helperUpload)
        {
            _context = context;
            this.helperUploadFiles=helperUpload;
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
        public async Task<IActionResult> Create(Companie companie, IFormFile archivo ,int ubicacion){
            if(ModelState.IsValid){ 
                string nombreArchivo = archivo.FileName;
                string path = "";

                 switch(ubicacion){

                case 0:
                    path = await this.helperUploadFiles.UploadFilesAsync(archivo, nombreArchivo, Folders.Uploads);
                break;
                case 1:
                    path = await this.helperUploadFiles.UploadFilesAsync(archivo, nombreArchivo, Folders.Images);
                break;
                case 2:
                    path = await this.helperUploadFiles.UploadFilesAsync(archivo, nombreArchivo, Folders.Documents);
                break;
                case 3:
                    path = await this.helperUploadFiles.UploadFilesAsync(archivo, nombreArchivo, Folders.Temp);
                break;
               

            }
                if(archivo!= null){
                   
                    companie.Logo = nombreArchivo;
                }
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