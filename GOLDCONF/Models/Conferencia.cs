using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoldConf.Models
{
    public class Conferencia
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        [Required]
        public int PonenteId { get; set; }
        [Required]
        public DateTime FechaConf { get; set; }
        [Required]
        public string TituloConf { get; set; }
        [Required]
        public decimal PrecioConf { get; set; }
        [Required]
        public string DetalleConf { get; set; }
        [Required]
        public string Ap1 { get; set; }
        [Required]
        public string Ap2 { get; set; }
        [Required]
        public string Ap3 { get; set; }
        [Required]
        public string Definicion { get; set; }
        [Required]
        public string Objetivo { get; set; }
        [Required]
        public string MetaFinal { get; set; }
        public Ponente Ponentes { get; set; }
        public List<Comprar> Compras { get; set; }
    }
}
