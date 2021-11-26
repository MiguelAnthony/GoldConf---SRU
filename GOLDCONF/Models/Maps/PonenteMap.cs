using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldConf.Models.Maps
{
    public class PonenteMap : IEntityTypeConfiguration<Ponente>
    {
        public void Configure(EntityTypeBuilder<Ponente> builder)
        {
            builder.ToTable("Ponente");
            builder.HasKey(o => o.Id);
        }
    }
}
