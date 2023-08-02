﻿using CommonLayer.Models;
using CommonLayer.RequestModels;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IUserRepo
    {
        public UserEntity Regsiter(RegistrationModel registrationModel);
        public string Login(LoginModel loginModel);
    }
}