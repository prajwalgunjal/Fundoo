using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using CommonLayer.Models;
using CommonLayer.RequestModels;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using RepositoryLayer.Services;
using System;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IBus _bus;
        private IUserBusiness iUserBusiness;
        public UserController (IUserBusiness iUserBusiness, IBus _bus)
        {
            this.iUserBusiness = iUserBusiness;
            this._bus = _bus;
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

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel loginModel)
        {
            var result = iUserBusiness.Login(loginModel);
            if (result != null)
            {
                return Ok(new ResponseModel<string> { Success = true, Message = "Login Successfull", Data = result });
            }
            else
                return BadRequest(new ResponseModel<string> { Success = false, Message = "Email Not Found", Data = null });
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> UserForgetPassword(string email)
        {
            
            try
            {
                if (iUserBusiness.CheckEmail(email))
                {
                    Send send = new Send();
                    ForgotPasswordModel forgotPasswordModel = iUserBusiness.UserForgotPassword(email);

                    Uri uri = new Uri("rabbitmq://localhost/FundooNotesEmail_Queue");
                    var endPoint = await _bus.GetSendEndpoint(uri);

                    await endPoint.Send(forgotPasswordModel);
                    send.SendingMail(forgotPasswordModel.Email, forgotPasswordModel.Token);

                    return Ok(new ResponseModel<string> { Success = true, Message = "Email sent ", Data = email });
                }
                else
                {
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Email Not send succesfull", Data = null });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
