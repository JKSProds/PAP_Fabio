using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Simple;
using QRCoder;

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
        public List<Utilizador> ObterUtilizadores(int? tipo)
        {
            List<Utilizador> LstUtilizador = new List<Utilizador>();

            using (Database db = ConnectionString)
            {
                using var result = db.Query("SELECT * FROM utilizadores where tipo="+tipo+";");
                while (result.Read())
                {
                    LstUtilizador.Add(new Utilizador()
                    {
                        ID = result["id_user"],
                        Nome = result["nome"],
                        Email = result["email"],
                        codigoAluno = result["codigoAluno"]
                    });
                }
            }

            return LstUtilizador;
        }

        public Utilizador ObterUtilizador(string email)
        {
            Utilizador res = new Utilizador();

            using (Database db = ConnectionString)
            {
                using var result = db.Query("SELECT * FROM utilizadores where email='" + email + "';");
                result.Read();
                if(result.Reader.HasRows)
                {
                    res = new Utilizador()
                    {
                        ID = result["id_user"],
                        Nome = result["nome"],
                        Email = result["email"],
                        Pass = result["pass"],
                        tipo = result["tipo"],
                        admin = result["admin"] == "1"
                    };
                }
                
            }

            return res;
        }

        public Utilizador ObterUtilizador(int ID)
        {
            Utilizador res = new Utilizador();

            using (Database db = ConnectionString)
            {
                using var result = db.Query("SELECT * FROM utilizadores where id_user ='" + ID + "';");
                result.Read();
                if (result.Reader.HasRows)
                {
                    res = new Utilizador()
                    {
                        ID = result["id_user"],
                        Nome = result["nome"],
                        Email = result["email"],
                        Pass = result["pass"],
                        tipo = result["tipo"],
                        admin = result["admin"] == "1",
                       codigoAluno = result["codigoAluno"]
                    };
                }

            }

            return res;
        }

        public string ObterCodigoQR(Utilizador u)
        {
            string res = "data:image/png;base64,";

            QRCodeGenerator qrcodegenerator = new QRCodeGenerator();

            QRCodeData qRCodeData = qrcodegenerator.CreateQrCode(u.codigoAluno, QRCodeGenerator.ECCLevel.Q);

            Base64QRCode qr = new Base64QRCode(qRCodeData);

            res += qr.GetGraphic(20);

            return res;
        }

        public Utilizador Editar(int ID)
        {
            Utilizador res = new Utilizador();

            using (Database db = ConnectionString)
            {
                using var result = db.Query("UPDATE * FROM utilizadores where id_user ='" + ID + "';");
                result.Read();
                if (result.Reader.HasRows)
                {
                    res = new Utilizador()
                    {
                        ID = result["id_user"],
                        Nome = result["nome"],
                        Email = result["email"],
                        Pass = result["pass"],
                        tipo = result["tipo"],
                    };
                }

            }

            return res;
        }
    }
}
