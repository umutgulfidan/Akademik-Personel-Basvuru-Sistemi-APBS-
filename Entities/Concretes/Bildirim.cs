using Core.Entities;
using Core.Entities.Concrete;
using System.ComponentModel.DataAnnotations.Schema;

public class Bildirim : IEntity
{
    public int Id { get; set; }  // Benzersiz kimlik
    public int KullaniciId { get; set; }  // Kullanıcı ID (Hangi kullanıcıya gidecek)
    [ForeignKey("KullaniciId")]
    public User Kullanici { get; set; }
    public string Baslik { get; set; }
    public string Aciklama { get; set; }  // Bildirim mesajı
    public string Icon { get; set; }  // Bildirim ikonu (Opsiyonel)
    public string Renk { get; set; }  // Bildirim rengi (Opsiyonel)
    public bool Status { get; set; }  // Okundu mu? (true = okundu, false = okunmadı)
    public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;  // Bildirim oluşturulma zamanı
}
