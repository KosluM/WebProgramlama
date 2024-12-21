namespace KuaforYonetimSistemi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } // Adı
        public string LastName { get; set; } // Soyadı
        public string Email { get; set; } // E-posta
        public string Password { get; set; } // Şifre
        public string Role { get; set; } // Rol
        public bool IsActive { get; set; } // Aktiflik Durumu
    }
}
