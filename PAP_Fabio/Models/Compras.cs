using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace PAP_Fabio.Models
{
    public class Compras
    {
        public int ID { get; set; }

        public DateTime Data { get; set; }

        public string IDProd { get; set; }

        public string NomeProd { get; set; }

        public string Quantidade { get; set; }

        public string Preco { get; set; }

        public int ID_User { get; set; }
    }
}
