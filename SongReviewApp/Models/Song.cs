namespace SongReviewApp.Models
{
    public class Song
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }
    }
}
