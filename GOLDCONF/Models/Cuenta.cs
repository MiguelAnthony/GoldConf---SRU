using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoldConf.Models
{
    public class Cuenta
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Currency { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public List<Transaccion> Transaccions { get; set; }
    }
}
