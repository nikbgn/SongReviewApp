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
            CreateMap<GenreDto, Genre>();

            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();

            CreateMap<Artist, ArtistDto>();
            CreateMap<ArtistDto, Artist>();

            CreateMap<Review, ReviewDto>();

            CreateMap<Reviewer, ReviewerDto>();
        }
    }
}
