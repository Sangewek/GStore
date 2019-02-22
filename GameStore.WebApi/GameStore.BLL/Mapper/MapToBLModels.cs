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

            CreateMap<Genre, BLGenre>()
            // .ForMember(dto => dto.Games, opt => opt.MapFrom(x => x.GameGenres.Select(y => y.Game).ToList())).MaxDepth(1)
            .ReverseMap();

            CreateMap<Platform, BLPlatform>()
            //.ForMember(dto => dto.Games,opt => opt.MapFrom(x => x.GamePlatform.Select(y => y.Game).ToList())).MaxDepth(1)
            .ReverseMap();

            CreateMap<Game, BLGame>()
             //.ForMember(dto => dto.Genres, opt => opt.MapFrom(x => x.GameGenres.Select(y => y.Genre).ToList())).MaxDepth(1)
            // .ForMember(dto => dto.Platforms, opt => opt.MapFrom(x => x.GamePlatform.Select(y => y.Platform).ToList())).MaxDepth(1)
            .ReverseMap();
        }
    }
}
