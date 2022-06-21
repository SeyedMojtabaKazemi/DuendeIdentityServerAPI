using Contract;
using Contract.UserAggregate;
using Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.Command.UserAggregate
{
    public class UserServiceCommand : IUserServiceCommand
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _UnitOfWork;

        public UserServiceCommand(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _UnitOfWork = unitOfWork;
        }

        public async Task<User> Add(UserDto user)
        {
            var newUser = new User
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CodeMelli = user.CodeMelli,
                Email = user.Email,
            };

            try
            {
                var aaa = await _userManager.CreateAsync(newUser, user.Password);
                _UnitOfWork.SaveChangeAsync();
                return newUser;
            }
            catch(Exception ex) { }
            return null;
        }

        public async Task UpdateSecurityStamp()
        {
            var user = await _userManager.FindByIdAsync("3");
            user.SecurityStamp = Convert.ToString(Guid.NewGuid());
            _UnitOfWork.SaveChangeAsync();
        }
    }
}
