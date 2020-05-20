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

        public bool Add_Data(User model)
        {
            try 
            {
                var result = user.Add_Data(model);
                if(!result.Equals(null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public bool UserLogin(User model)
        {
            try
            {
                var result = user.UserLogin(model);
                if (!result.Equals(null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            //return this.user.UserLogin(model);
        }

        public dynamic UserList()
        {
            return this.user.UserList();
        }

        public bool ForgotPassward(User model)
        {
            try
            {
                var result = user.ForgotPassward(model);
                if (!result.Equals(null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            // return this.user.ForgotPassward(model);
        }
    }
}
