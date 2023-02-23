namespace SongReviewApp.Models
{
    public class Artist
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public Country Country { get; set; }

        public ICollection<SongArtist> SongArtists { get; set; }
    }
}
