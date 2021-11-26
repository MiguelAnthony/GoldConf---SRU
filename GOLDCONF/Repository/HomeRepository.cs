using GoldConf.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldConf.Repository
{
    public interface IHomeRepository
    {
        List<Ponente> GetPonentes();
        List<Conferencia> GetConferencias();
    }
    public class HomeRepository : IHomeRepository
    {
        private readonly IGoldConfContext context;

        public HomeRepository(IGoldConfContext context)
        {
            this.context = context;
        }

        public List<Conferencia> GetConferencias()
        {
            var conferencias = context.Conferencias.
                Include(o => o.Ponentes).
                Where(o => o.FechaConf >= DateTime.Now).
                ToList();
            return conferencias;
        }

        public List<Ponente> GetPonentes()
        {
            var ponente = context.Ponentes.ToList();
            return ponente;
        }
    }
}
