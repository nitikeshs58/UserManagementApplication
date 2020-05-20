using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using UserBL.Interface;
using UserBL.Services;
using UserCL.Services;

namespace UserManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public InterfaceUserBusinessLayer user;

        public UserController(InterfaceUserBusinessLayer user)
        {
            this.user = user;
        }

        // ADD UserDetails API
        [HttpPost]
        public IActionResult Add_Data(User model)
        {
            try 
            {
                bool result = user.Add_Data(model);
                if(!result.Equals(null))
                {
                    var status = true;
                    var message = "User Details Added.";
                    return this.Ok(new { status,message });
                }
                else
                {
                    var status = false;
                    var message = "User Details not Added.";
                    return this.BadRequest(new { status, message });
                }
            }
            catch(Exception exception)
            {
                return BadRequest(new { error = exception.Message });
            }
        }

        // Login API
        [HttpPost("Login")]
        public IActionResult UserLogin(User model)
        {
            try
            {
                bool result = user.UserLogin(model);
                if (!result.Equals(null))
                {
                    var status = true;
                    var message = "User Login Successful.";
                    return this.Ok(new { status, message });
                }
                else
                {
                    var status = false;
                    var message = "User Login Failed.";
                    return this.BadRequest(new { status, message });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { error = exception.Message });
            }
            //string result = user.UserLogin(model);
            //return Ok(new { result });
        }

        /// <summary>
        /// for get UserList
        /// Returning UserList and whole data expect passward of user
        /// </summary>
        /// <returns></returns>
        [HttpGet("UserList")]
        public IActionResult UserList()
        {
            try
            {
                dynamic result = user.UserList();
                var status = true;
                var message = "AllUserList";
                return Ok(new {status , message , result });
            } 
            catch (Exception exception)
            {
                return BadRequest(new { error = exception.Message });
            }
        }

        // Forgot passward API
        [HttpPost("ForgotPassward")]
        public IActionResult ForgotPassward(User model)
        {
            try
            {
                bool result = user.ForgotPassward(model);
                if (!result.Equals(null))
                {
                    var status = true;
                    var message = "Passward is Changed, relogin with new Passward.";
                    return this.Ok(new { status, message });
                }
                else
                {
                    var status = false;
                    var message = "Cannot change passward.";
                    return this.BadRequest(new { status, message });
                }
            }
            catch (Exception exception)
            {
                return BadRequest(new { error = exception.Message });
            }
            //string result = user.ForgotPassward(model);
            //return Ok(new { result });
        }
    }
}