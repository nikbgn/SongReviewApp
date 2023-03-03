namespace SongReviewApp.Contracts
{
    using SongReviewApp.Models;

    public interface ISongRepository
    {
        /// <summary>
        /// Gets all songs.
        /// </summary>
        /// <returns>A collection of songs.</returns>
        Task<ICollection<Song>> GetSongs();

        /// <summary>
        /// Get a specific song by id.
        /// </summary>
        /// <param name="id">Identifier of a song.</param>
        /// <returns>A song with the requested id.</returns>
        Task<Song> GetSongById(int id);

        /// <summary>
        /// Get a song by name.
        /// </summary>
        /// <param name="name">Song name.</param>
        /// <returns>Song with the requested name.</returns>
        Task<Song> GetSongByName(string name);

        /// <summary>
        /// Checks if a song with that id exists.
        /// </summary>
        /// <param name="id">Id of the song.</param>
        /// <returns>True / False</returns>
        Task<bool> SongExists(int id);

        /// <summary>
        /// Creates a song.
        /// </summary>

        Task<bool> CreateSong(int artistId, int genreId, Song song);

        /// <summary>
        /// Updates song information.
        /// </summary>

        Task<bool> UpdateSong(int artistId, int genreId, Song song);
    }
}
