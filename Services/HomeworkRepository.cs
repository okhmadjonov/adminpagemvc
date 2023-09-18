using AdminPageMVC.Data;
using AdminPageMVC.DTO;
using AdminPageMVC.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository.Repositories;

public class HomeworkRepository : IHomeworkRepository
{
    private readonly AppDbContext _context;

    public HomeworkRepository(AppDbContext context) => _context = context;

    public async Task<List<HomeworkDTO>> GetAllHomeworkAsync()
    {
        var homework = await _context.Homeworks
            .Include(e => e.Task)
            .Select(e => new HomeworkDTO()
            {
                Id = e.Id,
                Description = e.Description,
                ImageUrl = e.ImageUrl,
                Task = e.Task
            })
            .ToListAsync();
        return homework;
    }

    public async Task<HomeworkDTO> GetHomeworkByIdAsync(int id)
    {
        var homeworkAsync = await _context.Homeworks
            .Include(e => e.Task)
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");
        var homework = new HomeworkDTO();
        homework.Id = id;
        homework.Description = homeworkAsync.Description;
        homework.ImageUrl = homeworkAsync.ImageUrl;
        homework.Task = homeworkAsync.Task;
        return homework;
    }

    public async Task AddHomeworkAsync(HomeworkDTO homeworkDto)
    {
        var homework = new Homework();
        homework.ImageUrl = homeworkDto.ImageUrl;
        homework.Description = homeworkDto.Description;
        var findLesson = await _context.Courses.FindAsync(homeworkDto.Task.Id);
        if (findLesson != null) homework.Task = homeworkDto.Task;
        _context.Homeworks.Add(homework);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateHomeworkAsync(int id, HomeworkDTO homeworkDto)
    {
        var homeworkFindAsync = await _context.Homeworks.FirstOrDefaultAsync(l => l.Id == id);
        homeworkFindAsync.Id = id;
        homeworkFindAsync.ImageUrl = homeworkDto.ImageUrl;
        homeworkFindAsync.Description = homeworkDto.Description;
        homeworkFindAsync.Task = homeworkDto.Task;

        _context.Entry(homeworkFindAsync).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteHomeworkAsync(int id)
    {
        var homework = await _context.Homeworks.FindAsync(id);
        if (homework != null)
        {
            _context.Homeworks.Remove(homework);
            await _context.SaveChangesAsync();
        }
    }
}