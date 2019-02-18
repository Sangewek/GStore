using AutoMapper;
using GameStore.BLL.Models;
using GameStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameStore.BLL.Mapper
{
    public class MapToBLModels : Profile
    {
        public MapToBLModels()
        {
            CreateMap<BLComment, Comment>().MaxDepth(1).ReverseMap();

            CreateMap<Publisher, BLPublisher>().MaxDepth(1).ReverseMap();

            CreateMap<Genre, BLGenre>().MaxDepth(1)
             .ForMember(dto => dto.Games, opt => opt.MapFrom(x => x.GameGenres.Select(y => y.Game).ToList()))
            .ReverseMap();

            CreateMap<Platform, BLPlatform>().MaxDepth(1)
            .ForMember(dto => dto.Games, opt => opt.MapFrom(x => x.GamePlatform.Select(y => y.Game).ToList()))
            .ReverseMap();

            CreateMap<Game, BLGame>().MaxDepth(1)
             .ForMember(dto => dto.Genres, opt => opt.MapFrom(x => x.GameGenres.Select(y => y.Genre).ToList())).MaxDepth(1)
             .ForMember(dto => dto.Platforms, opt => opt.MapFrom(x => x.GamePlatform.Select(y => y.Platform).ToList()))
            .ReverseMap();
        }
    }
}
