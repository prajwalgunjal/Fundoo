﻿using BusinessLayer.Interfaces;
using CommonLayer.Models;
using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBusiness : IUserBusiness
    {
        private IUserRepo userRepository;
        public UserBusiness(IUserRepo userRepository)
        {
            this.userRepository = userRepository;
        }
        public UserEntity Regsiter(RegistrationModel registrationModel)
        {
            return userRepository.Regsiter(registrationModel);
        }

        public string Login(LoginModel loginModel)
        {
            return userRepository.Login(loginModel);
        }
    }
}