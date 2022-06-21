using Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.UserAggregate
{
    public interface IUserServiceCommand
    {
        Task<User> Add(UserDto user);
        Task UpdateSecurityStamp();
    }
}
