namespace SongReviewApp.Models
{
    public class Song
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<SongArtist> SongArtists { get; set; }

        public ICollection<SongGenre> SongGenres { get; set; }
    }
}
