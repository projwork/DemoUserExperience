using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoUser.API.Entities;
using DemoUser.API.Models;
using DemoUser.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DemoUser.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> PostUser(CreateUserRequest request)
        {
            var user = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                BirthDate = request.BirthDate,
                Email = request.Email
            };

            _applicationDbContext.Users.Add(user);
            await _applicationDbContext.SaveChangesAsync();

            var userExperiences = (from e in request.Experiences
                select new UserExperience()
                {
                    FromDate = e.FromDate,
                    ToDate = e.ToDate,
                    Position = e.Position,
                    CompanyName = e.CompanyName,
                    UserId = user.UserId
                }).ToList();

            await _applicationDbContext.AddRangeAsync(userExperiences);
            await _applicationDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<UserSummaryResponse>> GetUsers()
        {
            var users = await (from u in _applicationDbContext.Users
                select new UserSummaryResponse()
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    BirthDate = u.BirthDate,
                    Address = u.Address
                }).ToListAsync();

            return users;
        }

        public async Task<CreateUserRequest> GetUserDetail(int userId)
        {
            var userDetail = await (from u in _applicationDbContext.Users
                select new CreateUserRequest()
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    BirthDate = u.BirthDate,
                    Address = u.Address,
                    Experiences = (from e in _applicationDbContext.UserExperiences.Where(e => e.UserId == u.UserId)
                            select new UserExperienceRequest()
                            {
                                FromDate = e.FromDate,
                                ToDate = e.ToDate,
                                Position = e.Position,
                                CompanyName = e.CompanyName
                            }).ToList()
                }).FirstOrDefaultAsync();

            return userDetail;
        }
    }
}
