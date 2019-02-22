using GameStore.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.DAL.EF.Configuration
{
    class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(s => s.Game).WithMany(s => s.Comments).HasForeignKey(x => x.GameId).IsRequired(true);
        }
    }
}
