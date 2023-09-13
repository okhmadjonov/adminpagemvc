namespace AdminPageMVC.Entities
{
    public class Result
    {
        public int Id { get; set; }
        public string? Url { get; set; }

        public Study Study { get; set; }

        public User User { get; set; }
    }
}
