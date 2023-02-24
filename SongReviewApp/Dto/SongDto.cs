namespace SongReviewApp.Dto
{
    public class SongDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }
    }
}
