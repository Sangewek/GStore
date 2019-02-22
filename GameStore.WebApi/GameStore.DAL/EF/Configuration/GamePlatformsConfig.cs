using GameStore.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.DAL.EF.Configuration
{
    class GamePlatformsConfig : IEntityTypeConfiguration<GamePlatforms>
    {
        public void Configure(EntityTypeBuilder<GamePlatforms> builder)
        {
            builder.HasKey(bc => new { bc.GameId, bc.PlatformId });
            builder.HasOne(bc => bc.Game).WithMany(b => b.GamePlatform).HasForeignKey(bc => bc.GameId);
            builder.HasOne(bc => bc.Platform).WithMany(c => c.GamePlatform).HasForeignKey(bc => bc.PlatformId);

        }
    }
}
