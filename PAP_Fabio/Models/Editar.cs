using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PAP_Fabio.Models
{
    public class Editar
    {
        [Display(Name = "Código")]
        public string ID_Aluno { get; set; }

        [Display(Name = "Nome")]
        public string Nome_Aluno { get; set; }

        [Display(Name = "Email")]
        public string Email_Aluno { get; set; }
    }
}
