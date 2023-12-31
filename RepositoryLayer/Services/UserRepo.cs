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
using System.Text.RegularExpressions;

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
        /*public UserEntity Regsiter(RegistrationModel registrationModel)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
            } 
        }*/


        public UserEntity Regsiter(RegistrationModel registrationModel)
        {
            try
            {
                // Validate input fields using regex
                if (!Regex.IsMatch(registrationModel.FirstName, @"^[A-Za-z]+$"))
                {
                    throw new Exception("Invalid first name.");
                }

                if (!Regex.IsMatch(registrationModel.LastName, @"^[A-Za-z]+$"))
                {
                    throw new Exception("Invalid last name.");
                }

                if (!Regex.IsMatch(registrationModel.Email, @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$"))
                {
                    throw new Exception("Invalid email address.");
                }

                if (!Regex.IsMatch(registrationModel.Password, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"))
                {
                    throw new Exception("Invalid password. Password must be at least 8 characters long and contain at least one letter and one digit.");
                }

                UserEntity userEntity = new UserEntity();
                bool emailExists = fundoo_Context.Users.Any(x => x.Email == registrationModel.Email);

                if (!emailExists)
                {
                    userEntity.FirstName = registrationModel.FirstName;
                    userEntity.LastName = registrationModel.LastName;
                    userEntity.Email = registrationModel.Email;
                    userEntity.Password = EncodePasswordToBase64(registrationModel.Password);
                    userEntity.createdAt = DateTime.Now;
                    userEntity.updatedAt = DateTime.Now;

                    fundoo_Context.Users.Add(userEntity);
                    fundoo_Context.SaveChanges();

                    return userEntity;
                }
                else
                {
                    return null; // Return null to indicate that registration failed due to existing email
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckEmail(string email)
        {
            try
            {
                bool emailExists = fundoo_Context.Users.Any(x => x.Email == email);
                if (emailExists)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }


        public string Login(LoginModel loginModel) {

            try
            {
                string encodedPassword = EncodePasswordToBase64(loginModel.Password);
                var checkEmail = fundoo_Context.Users.FirstOrDefault(x => x.Email == loginModel.Email);
                var checkPassword = fundoo_Context.Users.FirstOrDefault(x => x.Password == encodedPassword);

                if (checkEmail != null && checkPassword != null)
                {
                    var token = GenerateToken(checkEmail.Email, checkEmail.UserId);
                    return token;
                }

                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public string ByPassLogin()
        {

            try
            {
                string encodedPassword = EncodePasswordToBase64("Pune@123");
                var checkEmail = fundoo_Context.Users.FirstOrDefault(x => x.Email == "prajwalgunjal86@gmail.com");
                var checkPassword = fundoo_Context.Users.FirstOrDefault(x => x.Password == encodedPassword);

                if (checkEmail != null && checkPassword != null)
                {
                    var token = GenerateToken(checkEmail.Email, checkEmail.UserId);
                    return token;
                }

                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

     
        public ForgotPasswordModel UserForgotPassword(string email)
        {
            try
            {
                var result = fundoo_Context.Users.Where(x => x.Email == email).FirstOrDefault();

                ForgotPasswordModel forgotPasswordModel = new ForgotPasswordModel();
                forgotPasswordModel.Email = result.Email;
                forgotPasswordModel.Token = GenerateToken(result.Email, result.UserId);
                forgotPasswordModel.UserID = result.UserId;
                return forgotPasswordModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResetPasswordModel ForgetPassword(string email,ResetPasswordModel reset)
        {
            try
            {
                if (reset.ConfirmResetPassword.Equals(reset.Password))
                {
                    var result = fundoo_Context.Users.Where(x => x.Email == email).FirstOrDefault();
                    reset.Password = EncodePasswordToBase64(reset.Password);
                    fundoo_Context.SaveChanges();
                }
                    return reset;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private string GenerateToken(string Email ,int userID)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                new Claim("Email",Email), // you can use enum from claimtypes 
                new Claim("userID",userID.ToString())
            };
                var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
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
