namespace AdminPageMVC.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public required string? Name { get; set; }

        public required string? PhoneNumber { get; set; }

        public required DateTime DateTime { get; set; }
    }
}
