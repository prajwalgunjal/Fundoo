using CommonLayer.Models;
using RepositoryLayer.Entity;

namespace BusinessLayer.Interfaces
{
    public interface IUserBusiness
    {
        public UserEntity Regsiter(RegistrationModel registrationModel);
    }
}