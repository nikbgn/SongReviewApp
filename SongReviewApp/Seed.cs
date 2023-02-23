namespace SongReviewApp
{
    using SongReviewApp.Data;
    using SongReviewApp.Models;

    public class Seed
    {
        private readonly ApplicationDbContext dbContext;

        public Seed(ApplicationDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }

        public void SeedData()
        {
            if (!dbContext.SongArtists.Any())
            {
                var songArtists = new List<SongArtist>()
                {
                    new SongArtist()
                    {
                        Song  = new Song()
                        {
                            Name = "Cool Song",
                            ReleaseDate = new DateTime(2023,2,12),
                            SongGenres = new List<SongGenre>()
                            {
                                new SongGenre() { Genre = new Genre() {Name = "Rap"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review(){Title = "Best song ever", Text = "This is the greatest song I've ever heard of!", Reviewer = new Reviewer() {FirstName = "John", LastName = "Doe"}},
                                new Review(){Title = "Worst song ever", Text = "This is the worst song I've ever heard of!", Reviewer = new Reviewer() {FirstName = "John", LastName = "Boe"}},
                            }
                        },
                        Artist = new Artist()
                        {
                            Name = "RapStar",
                            Country = new Country(){Name = "Bulgaria"}
                        }
                    },

                    new SongArtist()
                    {
                        Song  = new Song()
                        {
                            Name = "Weird Song",
                            ReleaseDate = new DateTime(2000,2,1),
                            SongGenres = new List<SongGenre>()
                            {
                                new SongGenre() { Genre = new Genre() {Name = "Techno"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review(){Title = "Best song ever", Text = "This is the weirdest song I've ever heard of!", Reviewer = new Reviewer() {FirstName = "John", LastName = "Doe"}},
                                new Review(){Title = "Worst song ever", Text = "This is the dumbest song I've ever heard of!", Reviewer = new Reviewer() {FirstName = "John", LastName = "Boe"}},
                            }
                        },
                        Artist = new Artist()
                        {
                            Name = "The Weird Singer",
                            Country = new Country(){Name = "Estonia"}
                        }
                    }
                };

                dbContext.SongArtists.AddRange(songArtists);
                dbContext.SaveChanges();
            }
        }
    }
}
