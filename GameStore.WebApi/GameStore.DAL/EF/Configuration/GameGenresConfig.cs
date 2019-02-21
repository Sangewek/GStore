using GameStore.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.DAL.EF.Configuration
{
    class GameGenresConfig : IEntityTypeConfiguration<GameGenres>
    {
        public void Configure(EntityTypeBuilder<GameGenres> builder)
        {
            builder.HasKey(bc => new { bc.GenreId, bc.GameId });
            builder.HasOne(bc => bc.Genre).WithMany(b => b.GameGenres).HasForeignKey(bc => bc.GenreId);
            builder.HasOne(bc => bc.Game).WithMany(c => c.GameGenres).HasForeignKey(bc => bc.GameId);
        }
    }
}
