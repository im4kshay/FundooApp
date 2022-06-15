using BusinessLogicLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FundooApp.Controllers
{
    //[ApiController]
    //[Route("api/[Controller]")]

    public class UserController : Controller
    {
        private readonly IUserManager manager;

        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }

        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel userData) ////frombody attribute says value read from body of the request
        {
            try
            {
                string result = await this.manager.Register(userData);
                //this.logger.LogInformation("New user added successfully with userid " + userData.UserId + " & firstname:" + userData.FirstName);
                if (result.Equals("Registration Succesful"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/login")]
        public IActionResult Login([FromBody] LoginModel userData)
        {
            try
            {
                var result = this.manager.Login(userData);
                if (result != null)
                {
                    string token = this.manager.JWTTokenGeneration(userData.Email);
                    return this.Ok(new { Status = true, Message = "Login Successful", Data = result, Token = token});
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string> { Status = false, Message = "Login Failed" });
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPut]
        [Route("api/reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPsModel reset)
        {
            try
            {
                string result = await this.manager.ResetPassword(reset);
                if (result.Equals("Password Updated Successfully"))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = result });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new ResponseModel<string>() { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        [Route("api/forgot")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var result = this.manager.ForgetPassword(email);
                if (result.Equals(true))
                {
                    return this.Ok(new ResponseModel<string>() { Status = true, Message = "Forget password" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<string>() { Status = false, Message = "Bad Reuest forget password" });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}