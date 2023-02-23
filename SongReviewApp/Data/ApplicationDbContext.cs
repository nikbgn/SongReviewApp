namespace SongReviewApp.Data
{
    using Microsoft.EntityFrameworkCore;

    using SongReviewApp.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {

        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<SongArtist> SongArtists { get; set; }
        public DbSet<SongGenre> SongGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SongGenre>()
                .HasKey(sg => new { sg.SongId, sg.GenreId });

            modelBuilder.Entity<SongGenre>()
                .HasOne(s => s.Song)
                .WithMany(sg => sg.SongGenres)
                .HasForeignKey(s => s.SongId);


            modelBuilder.Entity<SongGenre>()
                .HasOne(s => s.Genre)
                .WithMany(sg => sg.SongGenres)
                .HasForeignKey(g => g.GenreId);



            modelBuilder.Entity<SongArtist>()
                .HasKey(sa => new { sa.SongId, sa.ArtistId });

            modelBuilder.Entity<SongArtist>()
                .HasOne(s => s.Song)
                .WithMany(sa => sa.SongArtists)
                .HasForeignKey(s => s.SongId);


            modelBuilder.Entity<SongArtist>()
                .HasOne(s => s.Artist)
                .WithMany(sg => sg.SongArtists)
                .HasForeignKey(g => g.ArtistId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
