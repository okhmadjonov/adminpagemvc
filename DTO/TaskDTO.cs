using AdminPageMVC.Entities;
using AdminPageMVC.Entities.enums;

namespace AdminPageMVC.DTO
{
    public class TaskDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public Lesson Lesson { get; set; }
        public EProcess Process { get; set; }
    }
}
