using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserBL.Interface;
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

        [HttpPost]
        public IActionResult Add_Data(User model)
        {

            string result = user.Add_Data(model);
            return Ok(new { result });
        }
    }
}