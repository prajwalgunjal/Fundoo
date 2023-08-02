using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserBusiness iUserBusiness;
        public UserController (IUserBusiness iUserBusiness)
        {
            this.iUserBusiness = iUserBusiness;
        }
        [HttpPost]
        // request url:-  localhost/Controller_name/MethodRoute
        [Route("Register")]
        public ActionResult Registeration(RegistrationModel registrationModel)
        {
            var result = iUserBusiness.Regsiter(registrationModel);
            if (result != null) {
                return Ok(new ResponseModel<UserEntity> { Success = true, Message = "Registred Successfull", Data = result });
            }
            else
            {
                return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Not Registred", Data = null });
            }
        }


    }
}
