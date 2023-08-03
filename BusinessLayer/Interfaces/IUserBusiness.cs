﻿using CommonLayer.Models;
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
    }



}