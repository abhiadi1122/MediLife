using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediLife.DataObjects;

namespace MediLife.DataAccess.IRepository
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User GetUserByEmail(string email);
        User ValidateUser(string userName, string passwordHash);
    }
}
