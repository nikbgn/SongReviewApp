﻿namespace SongReviewApp.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Text { get; set; } = null!;

        public Reviewer Reviewer { get; set; }

        public Song Song { get; set; }
    }
}
