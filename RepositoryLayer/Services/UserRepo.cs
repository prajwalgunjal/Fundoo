﻿using CommonLayer.Models;
using CommonLayer.RequestModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRepo : IUserRepo
    {
        private Fundoo_Context fundoo_Context;
        private readonly IConfiguration configuration;
        public UserRepo(Fundoo_Context fundoo_Context, IConfiguration configuration)
        {
            this.fundoo_Context = fundoo_Context;
            this.configuration = configuration;
        }
        public UserEntity Regsiter(RegistrationModel registrationModel)
        {
            UserEntity userEntity = new UserEntity();
            bool emailExists = fundoo_Context.Users.Any(x => x.Email == registrationModel.Email);
            userEntity.FirstName = registrationModel.FirstName;
            userEntity.LastName = registrationModel.LastName;
            userEntity.Email = registrationModel.Email;
            userEntity.Password = EncodePasswordToBase64(registrationModel.Password);
            userEntity.createdAt = DateTime.Now;
            userEntity.updatedAt = DateTime.Now;
            if (!emailExists)
            {
                fundoo_Context.Users.Add(userEntity);
                fundoo_Context.SaveChanges();
                return userEntity;
            }
            else
            {
                return null;
            }
            
        }

        public string Login(LoginModel loginModel) {
            string encodedPassword = EncodePasswordToBase64(loginModel.Password);
            var checkEmail = fundoo_Context.Users.FirstOrDefault(x => x.Email == loginModel.Email);
            var checkPassword = fundoo_Context.Users.FirstOrDefault(x => x.Password == encodedPassword);

            if (checkEmail  != null && checkPassword != null)
            {
                var token = GenerateToken(checkEmail.Email,checkEmail.UserId);
                return token;
            }

            else
            {
                return null;
            }
            
        }
        private string GenerateToken(string Email ,int userID)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Email",Email),
                new Claim("userID",userID.ToString())
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
    }
}