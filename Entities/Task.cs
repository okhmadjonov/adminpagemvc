using AdminPageMVC.Entities.enums;

namespace AdminPageMVC.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public Lesson Lesson { get; set; }
        public EProcess Process { get; set; }
    }
}
