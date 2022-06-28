using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace PAP_Fabio.Models
{
    public class Produtos
    {
        public int ID { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string Preco { get; set; }
    }

}
