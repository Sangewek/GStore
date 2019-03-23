using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.Models;
using GameStore.WebApi.ViewModels;

namespace GameStore.WebApi.Mapper
{
    public class MapToViewModels : Profile
    {
        public MapToViewModels()
        {
            CreateMap<BLGame, GameViewModel>()
                .ForMember(dto => dto.Platforms, opt => opt.MapFrom(x => x.Platforms.Select(y => y.Name).ToList()))
                .ForMember(dto => dto.Genres, opt => opt.MapFrom(x => x.Genres.Select(y => y.Name).ToList()));

            CreateMap<BLComment, CommentViewModel>();

            CreateMap<BLGenre, GenreViewModel>()
                .ForMember(dto => dto.Games, opt => opt.MapFrom(x => x.Games.Select(y => y.Name).ToList()))
                .ForMember(dto => dto.SubGenres, opt => opt.MapFrom(x => x.SubGenres.Select(y => y.Name).ToList()));

            CreateMap<BLPublisher, PublisherViewModel>()
                .ForMember(dto => dto.Games, opt => opt.MapFrom(x => x.Games.Select(y=>y.Name).ToList()));
        }
    }
}
