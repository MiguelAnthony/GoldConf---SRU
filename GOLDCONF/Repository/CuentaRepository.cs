using GoldConf.Models;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldConf.Repository
{
    public interface ICuentaRepository
    {
        List<Cuenta> GetCuentas();
        void NewTransaccion(Cuenta cuenta);
        List<Transaccion> GetTransaccions(int id);
        Cuenta GetCuentas(int id);
    }
    public class CuentaRepository : ICuentaRepository
    {
        private readonly IGoldConfContext context;

        public CuentaRepository(IGoldConfContext context)
        {
            this.context = context;
        }

        public List<Cuenta> GetCuentas()
        {
            var cuentas = context.Cuentas.ToList();
            return cuentas;
        }

        public void NewTransaccion(Cuenta cuenta)
        {
            context.Cuentas.Add(cuenta);
            context.SaveChanges();
        }

        public List<Transaccion> GetTransaccions(int id)
        {
            var transacciones = context.Transacciones.Where(o => o.CuentaId == id).ToList();
            return transacciones;
        }

        public Cuenta GetCuentas(int id)
        {
            var cuenta = context.Cuentas.First(o => o.Id == id);
            return cuenta;
        }
    }
}
