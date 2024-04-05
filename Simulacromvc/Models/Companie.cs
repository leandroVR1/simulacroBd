namespace Simulacromvc.Models
{
    public class Companie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nit { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Phone { get; set; }
        public string LegalRepresentative { get; set; }
        public long SectorId { get; set; } // Cambiado a long
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}