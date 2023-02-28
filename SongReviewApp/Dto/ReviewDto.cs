namespace SongReviewApp.Dto
{
    using SongReviewApp.Models;

    public class ReviewDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Text { get; set; } = null!;

    }
}
