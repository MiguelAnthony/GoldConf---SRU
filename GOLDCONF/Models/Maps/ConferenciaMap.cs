using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldConf.Models.Maps
{
    public class ConferenciaMap : IEntityTypeConfiguration<Conferencia>
    {

        public void Configure(EntityTypeBuilder<Conferencia> builder)
        {
            builder.ToTable("Conferencia");
            builder.HasKey(o => o.Id);

            builder.HasOne(o => o.Ponentes).
                WithMany(). //analizar si se va a usar el siguiente mencionado
                HasForeignKey(o => o.PonenteId);
        }
    }
}
