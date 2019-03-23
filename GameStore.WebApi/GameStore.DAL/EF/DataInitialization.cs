using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameStore.DAL.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace GameStore.DAL.EF
{
    public static class DataInitialization
    {
        public static void Initialize(DataContext context)
        {
            if (!context.Genre.Any())
            {
                var genres = new[]
                {
                    new Genre
                    {
                        Name = "Strategy"
                    },
                    new Genre {Name = "RTS",BaseGenreId=1},
                    new Genre {Name = "TBS",BaseGenreId=1},
                    new Genre {Name = "RPG"},
                    new Genre {Name = "Sports"},
                    new Genre
                    {
                        Name = "Races"                        
                    },
                    new Genre {Name = "rally",BaseGenreId=6},
                            new Genre {Name = "arcade",BaseGenreId=6},
                            new Genre {Name = "formula",BaseGenreId=6},
                            new Genre {Name = "off-road",BaseGenreId=6},
                    new Genre
                    {
                        Name = "Action"
                    },
                    new Genre {Name = "FPS",BaseGenreId=11},
                    new Genre {Name = "TPS",BaseGenreId=11},
                    new Genre {Name = "Adventure",BaseGenreId=11},
                    new Genre {Name = "Puzzle&Skill",BaseGenreId=11},
                    new Genre {Name = "Misc",BaseGenreId=11},
                };
                context.Genre.AddRange(genres);
                context.SaveChanges();
            }

            if (!context.Platform.Any())
            {
                var platforms = new[]
                {
                    new Platform {Name = "mobile"},
                    new Platform {Name = "browser"},
                    new Platform {Name = "desktop"},
                    new Platform {Name = "console"}
                };

                context.Platform.AddRange(platforms);
                context.SaveChanges();
            }

            if (!context.Publishers.Any())
            {
                var publishers = new[]
                {
                    new Publisher {Name = "Rockstar"},
                    new Publisher {Name = "Gameloft"},
                    new Publisher {Name = "Ubisoft"}
                };

                context.Publishers.AddRange(publishers);
                context.SaveChanges();
            }

            if (!context.Games.Any())
            {
                var games = new[]
                {
                    new Game
                    {
                        Name = "GTA V", Description = "GTA V Description", PublisherId=1,
                        GameGenres = new[] {new GameGenres {GameId = 1, GenreId = 1}},
                        GamePlatform = new[] {new GamePlatforms {GameId = 1, PlatformId = 2}}
                        ,Price = 200,DateOfAddition = DateTime.Now
                    },
                    new Game
                    {
                        Name = "Asphalt 5", Description = "Asphalt 5 Description", PublisherId=2,
                        GameGenres = new[]
                        {
                            new GameGenres {GameId = 1, GenreId = 1},
                            new GameGenres {GameId = 1, GenreId = 2},
                            new GameGenres {GameId = 1, GenreId = 4}
                        },
                        GamePlatform = new[]
                        {
                            new GamePlatforms {GameId = 2, PlatformId = 2},
                            new GamePlatforms {GameId = 2, PlatformId = 1},
                        },
                        Price = 20, DateOfAddition = DateTime.Now
                    },
                    new Game
                    {
                        Name = "Assassins Creed", Description = "Assassins Creed Description", PublisherId = 3,
                        GameGenres = new[]
                        {
                            new GameGenres {GameId = 1, GenreId = 1},
                            new GameGenres {GameId = 1, GenreId = 2},
                            new GameGenres {GameId = 1, GenreId = 4}
                        },
                        GamePlatform = new[]
                        {
                            new GamePlatforms {GameId = 2, PlatformId = 2},
                            new GamePlatforms {GameId = 2, PlatformId = 3},
                        },
                        Price = 300, DateOfAddition = DateTime.Now
                    },
                };

                context.Games.AddRange(games);
                context.SaveChanges();
            }

            if (!context.Comments.Any())
            {
                var comments = new[]
                {
                    new Comment {Body = "Comment For GTA V", GameId = 1, Name = "Vasya"},
                    new Comment {Body = "Comment 2 For GTA V", GameId = 1, Name = "Petya"},
                    new Comment {Body = "Comment For Asphalt", GameId = 2, Name = "Vasya"},
                    new Comment
                    {
                        Body = "Answer for Comment For GTA V", GameId = 1, Name = "Vasya", ParentCommentId=1
                    }

                };

                context.Comments.AddRange(comments);
                context.SaveChanges();
            }
        }
    }
}
