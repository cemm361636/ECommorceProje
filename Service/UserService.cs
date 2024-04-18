using Data;
using Entities;
using System.Linq;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext _context;

        public UserService(DatabaseContext context)
        {
            _context = context;
        }

        public User Authenticate(string email, string password)
        {
            // Veritabanında kullanıcıyı bulmak için e-posta adresini kullanın
            var user = _context.Users.SingleOrDefault(u => u.Email == email);

            // Kullanıcı bulunamazsa veya şifre eşleşmezse null döndürün
            if (user == null || user.Password != password)
                return null;

            // Doğrulama başarılı, kullanıcıyı döndürün
            return user;
        }

        public User Register(RegisterViewModel model)
        {
            // Kullanıcıyı kaydetme işlemini burada gerçekleştirin
            var user = new User
            {
                Name = model.FirstName,
                Surname = model.LastName,
                Email = model.Email,
                // Şifreleme işlemi için bir yöntem kullanabilirsiniz
                Password = model.Password
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void ForgotPassword(string email)
        {
            // Şifremi unuttum işlemini burada gerçekleştirin
            throw new NotImplementedException();
        }

        public bool ResetPassword(string email, string token, string newPassword)
        {
            // Şifre sıfırlama işlemini burada gerçekleştirin
            throw new NotImplementedException();
        }

        public string GetCartContent(int userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserCart(int userId, string cartContent)
        {
            throw new NotImplementedException();
        }

        public void ClearUserCart(int userId)
        {
            throw new NotImplementedException();
        }
    }
}