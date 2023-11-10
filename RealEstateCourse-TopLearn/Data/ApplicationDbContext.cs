using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstateCourse_TopLearn.Models;

namespace RealEstateCourse_TopLearn.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<UserModel> ApplicationUser { get; set; }
        public DbSet<EstateModel> Estate { get; set; }
    }
}