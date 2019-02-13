using GameStore.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.DAL.EF.Configuration
{
    class GameConfig : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasMany(bc => bc.Comments).WithOne(b => b.Game).IsRequired(false);
            builder.HasMany(bc => bc.GameGenres).WithOne(b => b.Game);
            builder.HasOne(bc => bc.Publisher).WithMany(b => b.Games).IsRequired(true);
        }
    }
}
