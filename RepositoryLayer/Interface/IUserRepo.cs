using CommonLayer.Models;
using CommonLayer.RequestModels;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IUserRepo
    {
        public UserEntity Regsiter(RegistrationModel registrationModel);
        public string Login(LoginModel loginModel);
        public bool CheckEmail(string email);
        public ForgotPasswordModel UserForgotPassword(string email);
        public ResetPasswordModel ForgetPassword(string email, ResetPasswordModel reset);
        public string ByPassLogin();
    }
}