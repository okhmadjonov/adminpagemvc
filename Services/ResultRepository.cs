using AdminPageMVC.Data;
using AdminPageMVC.DTO;
using AdminPageMVC.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository.Repositories;

public class ResultRepository : IResultRepository
{
    private readonly AppDbContext _context;

    public ResultRepository(AppDbContext context) => _context = context;


    public async Task<List<Result>> GetAllResultAsync()
    {
        var results = _context.Results.Include(e => e.User).Include(e => e.Study);
        return await results.ToListAsync();
    }

    public async Task<Result> GetResultByIdAsync(int id)
    {
        return await _context.Results
            .Include(e => e.User)
            .Include(e => e.Study)
            .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");
    }

    public async Task AddResultAsync(Result result)
    {
        _context.Results.Add(result);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteResultAsync(int id)
    {
        var result = await _context.Results.FindAsync(id);
        if (result != null)
        {
            _context.Results.Remove(result);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateResultAsync(Result result)
    {
        _context.Entry(result).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<List<ResultDTO>> GetUserResult(int userId)
    {
        var resultsForUser = _context.Results
            .Where(feedback => feedback.User.Id == userId)
            .Include(feedback => feedback.Study)
            .Include(feedback => feedback.User)
            .Select(feedback => new ResultDTO()
            {
                Id = feedback.Id,
                Url = feedback.Url,
                EducationId = feedback.Study.Id,
                UserId = feedback.User.Id
            })
            .ToList();
        return resultsForUser;
    }
}