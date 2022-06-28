using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace PAP_Fabio.Models
{
    public class Acessos
    {
        public int ID { get; set; }

        public DateTime Data { get; set; }

        public int ID_User { get; set; }
    }
}
