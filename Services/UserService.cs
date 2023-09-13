using AdminPageMVC.Data;
using AdminPageMVC.Entities;
using AdminPageMVC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdminPageMVC.Services
{
    public class UserService : IUserRepsoitory
    {
        public readonly AppDbContext _context;


        public UserService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<List<User>> GetUserList()
        {
            return await _context.Users.ToListAsync();

        }
    }
}
