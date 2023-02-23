namespace SongReviewApp.Models
{
    public class SongArtist
    {
        public int SongId { get; set; }

        public int ArtistId { get; set; }

        public Song Song { get; set; }

        public Artist Artist { get; set; }
    }
}
