using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using CommonLayer.Models;
using CommonLayer.RequestModels;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private ILogger<UserController> logger;
        public UserController (IUserBusiness iUserBusiness, IBus _bus, ILogger<UserController> logger)
        {
            this.iUserBusiness = iUserBusiness;
            this._bus = _bus;
            this.logger = logger;
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
            try
            {
                var result = iUserBusiness.Login(loginModel);
                if (result != null)
                {
                    return Ok(new ResponseModel<string> { Success = true, Message = "Login Successfull", Data = result });
                }
                
                else
                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Email Not Found", Data = null });
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        [Authorize]
        [HttpPost("Reset Password")]
        public IActionResult ResetPassword(ResetPasswordModel resetPassword)
        {
            try
            {
                string email = User.FindFirst(x => x.Type == "Email").Value;
                var result = iUserBusiness.ForgetPassword(email, resetPassword);
                if (result != null)
                {
                    return Ok(new ResponseModel<ResetPasswordModel> { Success = true, Message = "password reset succesfull", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<ResetPasswordModel> { Success = false, Message = "not reset succesfull", Data = null });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
            
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> UserForgetPassword(string email)
        {
            try
            {
                if (iUserBusiness.CheckEmail(email))
                {
                    SendEmail send = new SendEmail();
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
                logger.LogError(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }
    }
}


// resetpassword model  :- pass and confirm_password
// string email , resetPass 