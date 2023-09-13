using AdminPageMVC.Data;
using AdminPageMVC.Entities;
using AdminPageMVC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdminPageMVC.Services
{
    public class CourseService : ICourseRepository
    {
        public readonly AppDbContext _context;


        public CourseService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task<List<Course>> GetCourseList()
        {
            return await _context.Courses.ToListAsync();

        }
    }
}
