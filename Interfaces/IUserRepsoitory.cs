using AdminPageMVC.Entities;

namespace AdminPageMVC.Interfaces
{
    public interface IUserRepsoitory
    {
        Task<List<User>> GetUserList();
    }
}
