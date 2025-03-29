using Entities.Abstracts;

namespace Entities.Dtos.Bildirim
{
    public class UserBildirimQueryDto : BaseQueryDto
    {

        public int? Id { get; set; }
        public string? Baslik { get; set; }
        public bool? Status { get; set; }
        public DateTime? MinTarih { get; set; }
        public DateTime? MaxTarih { get; set; }
    }
}
