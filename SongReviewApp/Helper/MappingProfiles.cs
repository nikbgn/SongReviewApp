namespace SongReviewApp.Helper
{
    using AutoMapper;

    using SongReviewApp.Dto;
    using SongReviewApp.Models;

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Song, SongDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Country, CountryDto>();
            CreateMap<Artist, ArtistDto>();
            CreateMap<Review, ReviewDto>();
            CreateMap<Reviewer, ReviewerDto>();
        }
    }
}
