using GoldConf.Models;
using GoldConf.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GoldConf.Repository
{
    public interface IPonenteRepository
    {
        List<Ponente> GetPonentes();
        List<Ponente> GetPonentesSearch(string search);
        List<Cuenta> GetCuentaUser();
        List<Comprar> GetCompras();
        Ponente GetPonente(int id);
        List<Conferencia> GetConferenciasPonente(int id);
        void NewPonente(Ponente ponente, IFormFile file);
        void EditPonente(Ponente ponente, IFormFile file);
    }
    public class PonenteRepository : IPonenteRepository
    {
        private readonly IGoldConfContext context;
        public readonly IWebHostEnvironment hosting;

        public PonenteRepository(IGoldConfContext context, IWebHostEnvironment hosting)
        {
            this.context = context;
            this.hosting = hosting;
        }

        public List<Ponente> GetPonentes()
        {
            var ponentes = context.Ponentes.ToList();
            return ponentes;
        }

        public List<Ponente> GetPonentesSearch(string search)
        {
            var ponentes = context.Ponentes.Where(s => s.NomApe.Contains(search)).ToList(); ;
            return ponentes;
        }

        public List<Cuenta> GetCuentaUser()
        {
            var cuenta = context.Cuentas
                   .ToList();
            return cuenta;
        }

        public List<Comprar> GetCompras()
        {
            var compras = context.Compras.ToList();
            return compras;
        }

        public Ponente GetPonente(int id)
        {
            var ponente = context.Ponentes.
                Where(o => o.Id == id).
                FirstOrDefault();
            return ponente;
        }

        public List<Conferencia> GetConferenciasPonente(int id)
        {
            var conferencias = context.Conferencias
                .Where(o => o.PonenteId == id)
                .OrderByDescending(o => o.FechaConf)
                .ToList();
            return conferencias;
        }

        public void NewPonente(Ponente ponente, IFormFile file)
        {
            if (file != null)
                ponente.Imagen = SaveFile(file);
            else
                ponente.Imagen = null;
            context.Ponentes.Add(ponente);
            context.SaveChanges();
        }

        public void EditPonente(Ponente ponente, IFormFile file)
        {
            if (file != null)
                ponente.Imagen = SaveFile(file);
            else
                ponente.Imagen = null;
            context.Ponentes.Update(ponente);
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
    }
}
