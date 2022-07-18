using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoUser.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoUser.API.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserExperience> UserExperiences { get; set; }  
    }
}
