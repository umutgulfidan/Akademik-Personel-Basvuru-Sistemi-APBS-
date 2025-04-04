namespace Entities.Dtos.Ilan
{
    public class GetIlanAdminDto
    {
        public int Id { get; set; }
        public int OlusturanId { get; set; }
        public GetUserDto Olusturan { get; set; }
        public int PozisyonId { get; set; }
        public Entities.Concretes.Pozisyon Pozisyon { get; set; }
        public int BolumId { get; set; }
        public Entities.Concretes.Bolum Bolum { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public bool Status { get; set; }
    }
}
