using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAP_Fabio.Models
{
    public class Utilizador
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Pass { get; set; }
        public int tipo { get; set; }
        public bool admin { get; set; }
    }
}
