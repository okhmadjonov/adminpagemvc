using AdminPageMVC.Data;
using AdminPageMVC.DTO;
using AdminPageMVC.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<User>> GetAllUsersAsync() => await _context.Users.ToListAsync();

        public async Task<User> GetUserByIdAsync(int id) => await _context.Users.FirstOrDefaultAsync(u => u.Id == id) ?? throw new BadHttpRequestException("User not found");

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(int id, UserDTO userDto)
        {

            var firstOrDefaultAsync = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (firstOrDefaultAsync != null)
            {
                firstOrDefaultAsync.FullName = userDto.FullName;
                firstOrDefaultAsync.Email = userDto.Email;
                firstOrDefaultAsync.Password = userDto.Password;

                _context.Entry(firstOrDefaultAsync).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CourseDTO>> GetUserCourses(int id)
        {
            var user = await _context.Users
                .Include(e => e.Courses)
                .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("User Not found");

            var courseDtos = user.Courses.Select(course => new CourseDTO
            {
                Id = course.Id,
                ImageUrl = course.ImageUrl,
                Description = course.Description,
                Price = course.Price
            }).ToList();

            return courseDtos;
        }

        public async Task AddCourseToUser(int courseId)
        {
            var myId = GetMyId();
            var findAsync = await _context.Courses.FindAsync(courseId) ?? throw new BadHttpRequestException("Course not found");

            var user = await _context.Users.Include(e => e.Courses).FirstOrDefaultAsync(e => e.Id == Convert.ToInt32(myId)) ?? throw new BadHttpRequestException("User not found");

            user.Courses.Add(findAsync);

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmail(string email) => await _context.Users.FirstOrDefaultAsync(e => e.Email == email) ?? throw new BadHttpRequestException("User not found");

        public string GetMyId()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext is not null) result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return result ?? throw new BadHttpRequestException("User Id not found");
        }
    }
}
