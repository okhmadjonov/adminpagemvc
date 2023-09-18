
using AdminPageMVC.Data;
using AdminPageMVC.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository.Repositories;

public class TestRepository : ITestRepository
{
    private readonly AppDbContext _context;

    public TestRepository(AppDbContext context) => _context = context;

    public async Task<List<Test>> GetAll()
    {
        return await _context.Tests.ToListAsync();
    }

    public async Task<Test> GetTestById(int id)
    {
        return await _context.Tests.FirstOrDefaultAsync(u => u.Id == id) ?? throw new BadHttpRequestException("Test not found");
    }

    public async Task AddTestAsync(Test test)
    {
        _context.Tests.Add(test);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTestAsync(int id)
    {

        var test = await _context.Tests.FindAsync(id);
        if (test != null)
        {
            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateTest(Test test)
    {
        _context.Entry(test).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}