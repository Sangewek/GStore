using System;
using System.Collections.Generic;
using System.Text;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Services;
using GameStore.DAL.EF;
using GameStore.DAL.Interfaces;
using GameStore.DAL.Interfaces.Repositories;
using GameStore.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.BLL.Infrastructure
{
    public class InjectionResolver
    {

        public static void ConfigurateInjections(IServiceCollection service, string connection)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<IDataContext, DataContext>();
            service.AddDbContext<DataContext>(options =>
                options.UseLazyLoadingProxies().UseSqlServer(connection));

            service.AddScoped<IGameService, GameService>();
            service.AddScoped<ICommentService, CommentService >();
            service.AddScoped<IGenreService, GenreService>();
            service.AddScoped<IPublisherService, PublisherService>();

        }
    }
}
