using AdminPageMVC.Entities;

namespace AdminPageMVC.Interfaces
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetCourseList();
    }
}
