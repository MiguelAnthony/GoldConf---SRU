using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldConf.Models
{
    public class Comprar
    {
        public int Id { get; set; }
        public int IdConferencia { get; set; }
        public int IdUser { get; set; }

        public Conferencia Conferencia { get; set; }
        public User User { get; set; }
    }
}
