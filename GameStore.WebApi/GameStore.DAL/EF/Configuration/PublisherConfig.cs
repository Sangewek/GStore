using GameStore.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameStore.DAL.EF.Configuration
{
    class PublisherConfig : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasMany(bc => bc.Games).WithOne(b => b.Publisher).IsRequired(false);
            builder.HasIndex(e => e.Name).IsUnique();
        }
    }
}
