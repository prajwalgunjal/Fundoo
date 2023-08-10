using CommonLayer.Models;
using CommonLayer.RequestModels;
using RepositoryLayer.Entity;

namespace BusinessLayer.Interfaces
{
    public interface IUserBusiness
    {
        public UserEntity Regsiter(RegistrationModel registrationModel);
        public string Login(LoginModel loginModel);
        public bool CheckEmail(string email);
        public ForgotPasswordModel UserForgotPassword(string email);
        public ResetPasswordModel ForgetPassword(string email, ResetPasswordModel reset);
        public string ByPassLogin();
        public UserEntity LoginWithoutAutho(LoginModel loginModel);
    }



}