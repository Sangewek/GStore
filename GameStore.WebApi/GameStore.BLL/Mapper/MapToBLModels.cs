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
            CreateMap<BLComment, Comment>().ReverseMap().MaxDepth(3);

            CreateMap<Publisher, BLPublisher>().ReverseMap();

            CreateMap<Genre, BLGenre>().ForMember(dto => dto.Games,
                opt => opt.MapFrom(x => x.GameGenres.Select(y => y.Game).ToList())).MaxDepth(3)
            .ReverseMap();

            CreateMap<Platform, BLPlatform>().ForMember(dto => dto.Games,
            opt => opt.MapFrom(x => x.GamePlatform.Select(y => y.Game).ToList())).MaxDepth(3)
            .ReverseMap();

            CreateMap<Game, BLGame>().ForMember(dto => dto.Genres,
            opt => opt.MapFrom(x => x.GameGenres.Select(y => y.Genre).ToList())).MaxDepth(3)
            .ForMember(dto => dto.Platforms,
            opt => opt.MapFrom(x => x.GamePlatform.Select(y => y.Platform).ToList())).MaxDepth(3)
            .ReverseMap();
        }
    }
}
