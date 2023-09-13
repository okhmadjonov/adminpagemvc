namespace AdminPageMVC.Entities
{
    public class TaskAnswer
    {
        public int Id { get; set; }

        public string? Answer { get; set; }

        public string? FileUrl { get; set; }

        public Task Task { get; set; }
    }
}
