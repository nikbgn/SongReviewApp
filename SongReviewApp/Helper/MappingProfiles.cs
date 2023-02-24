﻿namespace SongReviewApp.Helper
{
    using AutoMapper;

    using SongReviewApp.Dto;
    using SongReviewApp.Models;

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Song, SongDto>();
        }
    }
}