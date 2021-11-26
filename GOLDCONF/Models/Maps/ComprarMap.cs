using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldConf.Models.Maps
{
    public class ComprarMap : IEntityTypeConfiguration<Comprar>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Comprar> builder)
        {
            builder.ToTable("CompraUsuario");
            builder.HasKey(o => o.Id);

            builder.HasOne(y => y.User)
                .WithMany(y => y.Compras)
                .HasForeignKey(y => y.IdUser);

            builder.HasOne(x => x.Conferencia)
                .WithMany(x => x.Compras)
                .HasForeignKey(x => x.IdConferencia);
        }
    }
}
