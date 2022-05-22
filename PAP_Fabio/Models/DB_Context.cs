using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Simple;

namespace PAP_Fabio.Models
{
    public class DB_Context
    {
        public string ConnectionString { get; set; }
        public DB_Context(string ConnectionString_)
        {
                ConnectionString = ConnectionString_;

                try
                {
                    Database db = ConnectionString;
                    Console.WriteLine("Connectado á Base de Dados MySQL com sucesso!");
                }
                catch
                {
                    Console.WriteLine("Não foi possivel conectar á BD MySQL!");
                }
        }

        public List<Utilizador> ObterAlunos()
        {
            List<Utilizador> LstUtilizador = new List<Utilizador>();

            using (Database db = ConnectionString)
            {
                using var result = db.Query("SELECT * FROM utilizadores where tipo=3;");
                while (result.Read())
                {
                    LstUtilizador.Add(new Utilizador()
                    {
                        ID = result["id_user"],
                        Nome = result["nome"]
                    });
                }
            }

            return LstUtilizador;
        }
    }
}
