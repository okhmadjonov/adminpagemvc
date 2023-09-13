namespace AdminPageMVC.DTO
{
    public class FeedbackDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int StudyId { get; set; }
        public int UserId { get; set; }
    }
}
