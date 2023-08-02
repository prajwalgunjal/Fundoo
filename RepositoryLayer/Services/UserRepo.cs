using CommonLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRepo : IUserRepo
    {
        private Fundoo_Context fundoo_Context;
        public UserRepo(Fundoo_Context fundoo_Context)
        {
            this.fundoo_Context = fundoo_Context;
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
