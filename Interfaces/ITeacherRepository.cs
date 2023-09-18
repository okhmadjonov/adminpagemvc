
using AdminPageMVC.DTO;
using AdminPageMVC.Entities;
using Task = System.Threading.Tasks.Task;

namespace AdminPageinMVC.Repository;

public interface ITeacherRepository
{
    Task<List<Teacher>> GetAllTeacherAsync();
    Task<Teacher> GetTeacherByIdAsync(int id);
    Task AddTeacherAsync(Teacher teacher);
    Task UpdateTeacherAsync(int id, TeacherDTO teacher);
    Task DeleteTeacherAsync(int id);

}