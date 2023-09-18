
using AdminPageMVC.Data;
using AdminPageMVC.DTO;
using AdminPageMVC.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository.Repositories;

public class TeacherRepository : ITeacherRepository
{
    private readonly AppDbContext _context;

    public TeacherRepository(AppDbContext context) => _context = context;

    public Task<List<Teacher>> GetAllTeacherAsync()
    {
        return _context.Teachers.ToListAsync();
    }

    public async Task<Teacher> GetTeacherByIdAsync(int id)
    {
        return await _context.Teachers.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddTeacherAsync(Teacher teacher)
    {
        _context.Teachers.Add(teacher);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTeacherAsync(int id, TeacherDTO teacher)
    {
        var firstOrDefaultAsync = await _context.Teachers.FirstOrDefaultAsync(u => u.Id == id);

        if (firstOrDefaultAsync != null)
        {
            firstOrDefaultAsync.Name = teacher.Name;
            firstOrDefaultAsync.Type = teacher.Type;
            firstOrDefaultAsync.ImageUrl = teacher.ImageUrl;
            _context.Entry(firstOrDefaultAsync).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteTeacherAsync(int id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher != null)
        {
            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();
        }
    }
}