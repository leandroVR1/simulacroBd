using Microsoft.EntityFrameworkCore;
using Simulacromvc.Models;


namespace Simulacromvc.Data{
    public class CompanieContext : DbContext
    {
        public CompanieContext(DbContextOptions<CompanieContext> options) : base(options)
       
        {
        }
        public DbSet<Companie> Companies { get; set; }
        
    }
    
}
