using System;
using System.Collections.Generic;
using System.Text;
using UserCL.Services;

namespace UserRL.Interface
{
    public interface InterfaceUserRepositoryLayer
    {
        bool Add_Data(User model);
        bool UserLogin(User model);
        dynamic UserList();
        bool ForgotPassward(User model);
    }
}
