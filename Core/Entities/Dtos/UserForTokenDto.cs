namespace Core.Entities.Dtos
{
    public class UserForTokenDto
    {
        public int UserId { get; set; }  // Kullanıcı ID'si
        public string FirstName { get; set; }  // Kullanıcı Adı
        public string LastName { get; set; }  // Kullanıcı Soyadı
        public string Email { get; set; }  // Kullanıcı E-posta Adresi
        public string NationalityId { get; set; }  // Kullanıcı Milliyet ID'si
        public string Token { get; set; }  // Token bilgisi (eğer varsa)
        public List<string> Roles { get; set; } = new List<string>();  // Boş bir liste ile başlatıyoruz.
    }

}