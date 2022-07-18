using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoUser.API.Models;

namespace DemoUser.API.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> PostUser(CreateUserRequest request);
        Task<IEnumerable<UserSummaryResponse>> GetUsers();
        Task<CreateUserRequest> GetUserDetail(int userId);
    }
}
