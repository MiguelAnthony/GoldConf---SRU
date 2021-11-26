using GoldConf.Models.Maps;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldConf.Models
{
    public interface IGoldConfContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Conferencia> Conferencias { get; set; }
        DbSet<Ponente> Ponentes { get; set; }
        DbSet<Comprar> Compras { get; set; }
        DbSet<Cuenta> Cuentas { get; set; }
        DbSet<Transaccion> Transacciones { get; set; }
        int SaveChanges();
    }
    public class GoldConfContext : DbContext, IGoldConfContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Conferencia> Conferencias { get; set; }
        public DbSet<Ponente> Ponentes { get; set; }
        public DbSet<Comprar> Compras { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }

        public GoldConfContext(DbContextOptions<GoldConfContext> options)
           : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Esto se hace por cada tabla de base de datos
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new ConferenciaMap());
            modelBuilder.ApplyConfiguration(new PonenteMap());
            modelBuilder.ApplyConfiguration(new ComprarMap());
            modelBuilder.ApplyConfiguration(new CuentaMap());
            modelBuilder.ApplyConfiguration(new TransaccionMap());

        }
    }
}
