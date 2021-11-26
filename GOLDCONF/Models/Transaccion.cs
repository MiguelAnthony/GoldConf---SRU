using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoldConf.Models
{
    public class Transaccion
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public int CuentaId { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Tipo { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public DateTime FechaHora { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Motivo { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public decimal Amount { get; set; }
    }
}
