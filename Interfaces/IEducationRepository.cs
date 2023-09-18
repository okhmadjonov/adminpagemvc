
using AdminPageMVC.DTO;
using AdminPageMVC.Entities;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository;

public interface IEducationRepository
{
    Task<List<StudyDTO>> GetAllEducationAsync();
    Task<Study> GetEducationByIdAsync(int id);
    Task AddEducationAsync(StudyDTO educationDto);
    Task UpdateEducationAsync(int id, StudyDTO educationDto);
    Task DeleteEducationAsync(int id);
}