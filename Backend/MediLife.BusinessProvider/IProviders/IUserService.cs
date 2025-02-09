using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediLife.DataObjects;

namespace MediLife.BusinessProvider.IProviders
{
    public interface IUserService
    {
        void RegisterUser(User user);
        User GetUserByEmail(string email);
        string Authenticate(UserLoginRequest user);
    }
}
