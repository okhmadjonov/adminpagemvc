namespace AdminPageMVC.Entities
{
    public class Test
    {
        public int Id { get; set; }
        public string? Question { get; set; }
        public List<string>? Variants { get; set; }
        public string? RightVariant { get; set; }
    }
}
