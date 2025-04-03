namespace Entities.Dtos.AlanKriteri
{
    public class UpdateAlanKriteriDto
    {
        public int Id { get; set; }
        public int KriterId { get; set; }

        public int AlanId { get; set; }

        public int PozisyonId { get; set; }

        public int? MinAdet { get; set; }
    }

}
