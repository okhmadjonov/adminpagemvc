namespace AdminPageMVC.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string? Description { get; set; }

        public Study Study { get; set; }

        public User User { get; set; }
    }
}
