namespace Entities.Dtos.Bolum
{
    public class UpdateBolumDto
    {
        public int Id { get; set; }

        public int AlanId { get; set; }
        public string Ad { get; set; }
        public string? Aciklama { get; set; }
        public string? Telefon { get; set; }
        public string? Email { get; set; }
        public string? Adres { get; set; }
    }
}
