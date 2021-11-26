using GoldConf.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GoldConf.Repository
{
    public interface IConferenciaRepository
    {
        List<Ponente> GetPonentes();
        List<Conferencia> GetConferencias();
        List<Conferencia> GetConferenciasOrder();
        List<Comprar> GetComprasOrderPasado(int idUser);
        List<Comprar> GetComprasOrderFuturas(int idUser);
        List<Conferencia> GetConferenciasOrderUC();
        List<Cuenta> GetCuentas();
        List<Comprar> GetCompras();
        void NewConferencia(Conferencia conferencia, IFormFile file);
        void EditConferencia(Conferencia conferencia, IFormFile file);
        void AddTransaccion(Transaccion transaccionCli, Transaccion transaccionAd);
        void ModificaMontoCuenta(int cuentaId);
        void AddCompras(int userId, int conferenciaId);
        User GetUser(int id);
    }
    public class ConferenciaRepository : IConferenciaRepository
    {
        private readonly IGoldConfContext context;
        public readonly IWebHostEnvironment hosting;

        public ConferenciaRepository(IGoldConfContext context, IWebHostEnvironment hosting)
        {
            this.context = context;
            this.hosting = hosting;
        }

        public List<Comprar> GetCompras()
        {
            var conferencias = context.Compras.ToList();
            return conferencias;
        }

        public List<Conferencia> GetConferencias()
        {
            var conferencias = context.Conferencias.ToList();
            return conferencias;
        }

        public List<Conferencia> GetConferenciasOrder()
        {
            var conferencias = context.Conferencias
                    .Include(o => o.Ponentes)
                    .OrderByDescending(o => o.FechaConf)
                    .Where(o => o.FechaConf >= DateTime.Now)
                    .ToList();
            return conferencias;
        }

        public List<Comprar> GetComprasOrderPasado(int idUser)
        {
            var conferencias = context.Compras
                    .Include(o => o.Conferencia)
                    .Where(o => o.IdUser == idUser)
                    .Where(o => o.Conferencia.FechaConf < DateTime.Now)
                    .OrderByDescending(o => o.Conferencia.FechaConf)
                    .ToList();
            return conferencias;
        }

        public List<Comprar> GetComprasOrderFuturas(int idUser)
        {
            var conferencias = context.Compras
                    .Include(o => o.Conferencia)
                    .Where(o => o.IdUser == idUser)
                    .Where(o => o.Conferencia.FechaConf > DateTime.Now)
                    .OrderByDescending(o => o.Conferencia.FechaConf)
                    .ToList();
            return conferencias;
        }

        public List<Conferencia> GetConferenciasOrderUC()
        {
            var conferencias = context.Conferencias
                    .Include(o => o.Ponentes)
                    .OrderByDescending(o => o.FechaConf)
                    .ToList();
            return conferencias;
        }

        public List<Cuenta> GetCuentas()
        {
            var cuentas = context.Cuentas.ToList();
            return cuentas;
        }

        public List<Ponente> GetPonentes()
        {
            var ponentes = context.Ponentes.ToList();
            return ponentes;
        }

        public void NewConferencia(Conferencia conferencia, IFormFile file)
        {
            if (file != null)
                conferencia.ImagePath = SaveFile(file);
            else
                conferencia.ImagePath = null;
            context.Conferencias.Add(conferencia);
            context.SaveChanges();
        }

        public void EditConferencia(Conferencia conferencia, IFormFile file)
        {
            if (file != null)
                conferencia.ImagePath = SaveFile(file);
            else
                conferencia.ImagePath = null;
            context.Conferencias.Update(conferencia);
            context.SaveChanges();
        }

        public void AddTransaccion(Transaccion transaccionCli, Transaccion transaccionAd)
        {
            context.Transacciones.Add(transaccionCli);
            context.Transacciones.Add(transaccionAd);
            context.SaveChanges();
        }

        public void ModificaMontoCuenta(int cuentaId)
        {
            var transaccions = context.Transacciones.ToList();
            var cuenta = context.Cuentas
                .Include(o => o.Transaccions)
                .FirstOrDefault(o => o.Id == cuentaId);

            var total = cuenta.Transaccions.Sum(o => o.Amount);
            cuenta.Amount = total;
            context.SaveChanges();
        }

        public void AddCompras(int userId, int conferenciaId)
        {
            var comprar = new Comprar();
            comprar.IdUser = userId;
            comprar.IdConferencia = conferenciaId;
            context.Compras.Add(comprar);
            context.SaveChanges();
        }

        private string SaveFile(IFormFile file)
        {
            string relativePath = "";

            if (file.Length > 0 && (file.ContentType == "image/jpeg" || file.ContentType == "image/png" || file.ContentType == "image/gif"))
            {
                relativePath = Path.Combine("files", file.FileName);
                var filePath = Path.Combine(hosting.WebRootPath, relativePath);
                var stream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(stream);
                stream.Close();
            }

            return "/" + relativePath.Replace('\\', '/');
        }

        public User GetUser(int id)
        {
            return context.Users.Where(o => o.Id == id).FirstOrDefault();
        }
    }
}
