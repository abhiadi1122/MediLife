using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediLife.BusinessProvider.IProviders;
using MediLife.DataAccess.IRepository;
using MediLife.DataObjects;

namespace MediLife.BusinessProvider.Providers
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void RegisterUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Email) || string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                throw new ArgumentException("Email and password cannot be empty.");
            }
            
            user.PasswordHash = HashPassword(user.PasswordHash);

            _userRepository.AddUser(user);
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        private string HashPassword(string password)
        {            
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }

    }
}
