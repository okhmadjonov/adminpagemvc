﻿namespace AdminPageMVC.Entities
{
    public class Homework
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }

        public string? Description { get; set; }

        public int? TaskId { get; set; }
        public Task Task { get; set; }
    }
}
