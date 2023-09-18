using AdminPageMVC.Data;
using AdminPageMVC.DTO;
using AdminPageMVC.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly AppDbContext _context;

        public FeedbackRepository(AppDbContext context) => _context = context;


        public async Task<List<FeedbackDTO>> GetAllFeedbackAsync()
        {
            var feedbackDtos = await _context.Feedback
                .Include(e => e.User)
                .Include(e => e.Study)
                .Select(e => new FeedbackDTO()
                {
                    Id = e.Id,
                    Description = e.Description,
                    UserId = e.User.Id,
                    StudyId = e.Study.Id
                })
                .ToListAsync();

            return feedbackDtos;
        }

        public async Task<FeedbackDTO> GetFeedbackByIdAsync(int id)
        {
            var firstOrDefaultAsync = await _context.Feedback
                .Include(e => e.User)
                .Include(e => e.Study)
                .FirstOrDefaultAsync(e => e.Id == id) ?? throw new BadHttpRequestException("Not Found");
            FeedbackDTO feedbackDto = new FeedbackDTO();
            feedbackDto.Id = id;
            feedbackDto.Description = firstOrDefaultAsync.Description;
            feedbackDto.UserId = firstOrDefaultAsync.User.Id;
            feedbackDto.StudyId = firstOrDefaultAsync.Study.Id;
            return feedbackDto;
        }

        public async Task AddFeedbackAsync(ClaimsPrincipal principal, FeedbackDTO feedbackDto)
        {
            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            Review feedback = new Review();
            feedback.Description = feedbackDto.Description;
            feedback.Study = await _context.Studies.FindAsync(feedbackDto.StudyId) ?? throw new BadHttpRequestException("Education not found");
            feedback.User = await _context.Users.FindAsync(Convert.ToInt32(userId)) ?? throw new BadHttpRequestException("User not found");
            _context.Feedback.Add(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFeedbackAsync(int id)
        {
            var feedback = await _context.Feedback.FindAsync(id);
            if (feedback != null)
            {
                _context.Feedback.Remove(feedback);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<FeedbackDTO>> GetUserFeedbacks(int userId)
        {
            var feedbacksForUser = _context.Feedback
                .Where(feedback => feedback.User.Id == userId)
                .Include(feedback => feedback.Study) // Eagerly load the Education entity
                .Include(feedback => feedback.User) // Eagerly load the Education entity
                .Select(feedback => new FeedbackDTO
                {
                    Id = feedback.Id,
                    Description = feedback.Description,
                    StudyId = feedback.Study.Id,
                    UserId = feedback.User.Id
                })
                .ToList();
            return feedbacksForUser;
        }
    }
}
