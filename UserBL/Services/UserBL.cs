using System;
using System.Collections.Generic;
using System.Text;
using UserBL.Interface;
using UserCL.Services;
using UserRL.Interface;

namespace UserBL.Services
{
    public class UserBusinessLayer : InterfaceUserBusinessLayer
    {

        private InterfaceUserRepositoryLayer user;
        public UserBusinessLayer(InterfaceUserRepositoryLayer user)
        {
            this.user = user;
        }

        public string Add_Data(User model)
        {
            return this.user.Add_Data(model);
        }
    }
}
