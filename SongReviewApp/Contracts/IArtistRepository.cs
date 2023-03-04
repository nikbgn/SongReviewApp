namespace SongReviewApp.Contracts
{
    using SongReviewApp.Models;

    public interface IArtistRepository
    {
        /// <summary>
        /// Gets artists.
        /// </summary>
        /// <returns>Collection of artists.</returns>
        Task<ICollection<Artist>> GetArtists();

        /// <summary>
        /// Gets artist by id.
        /// </summary>
        /// <param name="artistId">Artist id.</param>
        /// <returns>Artist.</returns>
        Task<Artist> GetArtistById(int artistId);

        /// <summary>
        /// Get artist of a song.
        /// </summary>
        /// <param name="songId">Song id.</param>
        /// <returns>Collection of artists.</returns>
        Task<ICollection<Artist>> GetArtistOfASong(int songId);

        /// <summary>
        /// Gets songs by artist.
        /// </summary>
        /// <param name="artistId">Artist id.</param>
        /// <returns>Collection of songs.</returns>
        Task<ICollection<Song>> GetSongByArtist(int artistId);

        /// <summary>
        /// Checks if artist exists.
        /// </summary>
        /// <param name="artistId">Artist id.</param>
        /// <returns>True / False</returns>
        Task<bool> ArtistExists(int artistId);

        /// <summary>
        /// Creates an artist.
        /// </summary>

        Task<bool> CreateArtist(Artist artist);

        /// <summary>
        /// Updates artist.
        /// </summary>

        Task<bool> UpdateArtist(Artist artist);

        /// <summary>
        /// Deletes artist
        /// </summary>

        Task<bool> DeleteArtist(Artist artist);

    }
}
