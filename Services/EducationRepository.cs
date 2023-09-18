using AdminPageMVC.Data;
using AdminPageMVC.DTO;
using AdminPageMVC.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository.Repositories;

public class EducationRepository : IEducationRepository
{
    private readonly AppDbContext _context;
    public EducationRepository(AppDbContext context) => _context = context;

    public async Task<List<StudyDTO>> GetAllEducationAsync()
    {
        var education = await _context.Studies
            .Include(e => e.Course)
            .Select(e => new StudyDTO()
            {
                Id = e.Id,
                Description = e.Description,
                Course = e.Course,
                Finish = e.Finish,
                Title = e.Title
            })
            .ToListAsync();
        return education;
    }

    public async Task<Study> GetEducationByIdAsync(int id)
    {
        var education = await _context.Studies
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");
        var educationDto = new StudyDTO();
        educationDto.Id = id;
        educationDto.Description = education.Description;
        educationDto.Title = education.Title;
        educationDto.Finish = education.Finish;
        educationDto.Course = education.Course;
        return education;
    }

    public async Task AddEducationAsync(StudyDTO educationDto)
    {
        var education = new Study();
        education.Description = educationDto.Description;
        education.Finish = educationDto.Finish;
        education.Title = educationDto.Title;
        var findCourse = await _context.Courses.FindAsync(educationDto.Course.Id);
        if (findCourse != null) education.Course = educationDto.Course;
        _context.Studies.Add(education);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEducationAsync(int id, StudyDTO educationDto)
    {
        var educationAsync = await _context.Studies.FirstOrDefaultAsync(e => e.Id == id);
        educationAsync.Id = id;
        educationAsync.Description = educationDto.Description;
        educationAsync.Title = educationDto.Title;
        educationAsync.Finish = educationDto.Finish;
        educationAsync.Course = educationDto.Course;

        _context.Entry(educationAsync).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEducationAsync(int id)
    {
        var education = await _context.Studies.FindAsync(id);
        if (education != null)
        {
            _context.Studies.Remove(education);
            await _context.SaveChangesAsync();
        }
    }
}