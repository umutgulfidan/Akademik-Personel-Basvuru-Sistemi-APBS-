namespace Entities.Dtos.PuanKriteri
{
    public class UpdatePuanKriteriDto
    {
        public int Id { get; set; }
        public int KriterId { get; set; }

        public int AlanId { get; set; }

        public int PozisyonId { get; set; }

        public int? MinPuan { get; set; }
        public int? MaxPuan { get; set; }
    }
}
