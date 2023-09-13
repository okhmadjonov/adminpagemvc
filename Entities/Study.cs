namespace AdminPageMVC.Entities
{
    public class Study
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public string? Description { get; set; }
        public string? Finish { get; set; }
        public Course? Course { get; set; }
    }
}
