using Entities;

namespace Service
{
    public interface IUserService
    {
        // Kullanıcı doğrulaması için metot
        User Authenticate(string email, string password);

        // Kullanıcı kaydı için metot
        User Register(RegisterViewModel model);

        // Şifre sıfırlama için metotlar
        void ForgotPassword(string email);
        bool ResetPassword(string email, string token, string newPassword);
    }
}