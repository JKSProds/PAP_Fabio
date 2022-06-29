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

        public Utilizador ObterEmail(string Email)
        {
            Utilizador res = new Utilizador();

            using (Database db = ConnectionString)
            {
                using var result = db.Query("SELECT * FROM utilizadores where email ='" + Email + "';");
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

        public bool ExisteEmailDuplicado(string Email)
        {
            if (String.IsNullOrEmpty(Email)) return false;

            int res = 0;

            using (Database db = ConnectionString)
            {
                using var result = db.Query("SELECT Count(*) as Count FROM utilizadores where email = '" + Email + "';");

                result.Read();

                res = result["Count"];

            }
            return res > 0;

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

        public Utilizador Apagar(int ID)
        {
            Utilizador res = new Utilizador();

            using (Database db = ConnectionString)
            {
                using var result = db.Query("Delete from utilizadores where id_user='" + ID + "';");
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

        /*public Acessos ApagarAcess(int ID)
        {
            Acessos res = new Acessos();

            using (Database db = ConnectionString)
            {
                using var result = db.Query("Delete from acessos where id_acesso='" + ID + "';");
                result.Read();
                if (result.Reader.HasRows)
                {
                    res = new Acessos()
                    {
                        ID = result["id_acesso"],
                        ID_User = result["id_user"],
                        Data = result["data"],
                    };
                }
            }

            return res;
        }*/

        public Utilizador Novo(int ID)
        {
            Utilizador res = new Utilizador();

            using (Database db = ConnectionString)
            {
                using var result = db.Query("INSERT INTO utilizadores where id_user ='" + ID + "';");

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

        public void EditarUtil(Utilizador util)
        {
            Database db = ConnectionString;
            String sql = "UPDATE utilizadores set nome='" + util.Nome + "', email='" + util.Email + "', admin='" + (util.admin ? 1 : 0) + "' where id_user='" + util.ID+"';";
            db.Execute(sql);
            db.Connection.Close();
        }

        public void NovoUtil(Utilizador util)
        {
            Database db = ConnectionString;
            String sql = "INSERT INTO utilizadores (id_user, email, pass, nome, admin, tipo, codigoAluno) values (" + util.ID + ", '" + util.Email + "','" + util.Pass + "', '" + util.Nome + "', " + (util.admin ? 1 : 0) + ", " + util.tipo + ", '" + util.codigoAluno + "');";

            db.Execute(sql);
            db.Connection.Close();
        }

       
        public void ApagarUser(string id)
        {
            Database db = ConnectionString;
            String sql = "Delete from utilizadores where id_user='" + id + "';";
            db.Execute(sql);
            db.Connection.Close();
        }

        public void ApagarCompras(string id)
        {
            Database db = ConnectionString;
            String sql = "Delete from Compras where id_compra='" + id + "';";
            db.Execute(sql);
            db.Connection.Close();
        }

        public void ApagarBar(string id)
        {
            Database db = ConnectionString;
            String sql = "Delete from compras_bar where id_compra_bar='" + id + "';";
            db.Execute(sql);
            db.Connection.Close();
        }


        public void ApagarAcessos(string id)
        {
            Database db = ConnectionString;
            String sql = "Delete from acessos where id_acesso='" + id + "';";
            db.Execute(sql);
            db.Connection.Close();
        }

        public Utilizador ObterUtil(int idutil)
        {
            Utilizador util = new Utilizador();
            Database db = ConnectionString;

            var result = db.Query("SELECT * FROM utilizadores where id_user ='" + idutil + "';");

            while (result.Read())
            {
                util = new Utilizador()
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

            db.Connection.Close();         

            return util;
        }

        public List<Compras> ObterComprasTeste()
        {
            List<Compras> LstCompras = new List<Compras>();

            using (Database db = ConnectionString)
            {
                using var result = db.Query("SELECT * FROM Compras where tipo=3;");
                while (result.Read())
                {
                    LstCompras.Add(new Compras()
                    {
                        ID = result["id_compra"],
                        NomeProd = result["nome_prod"],
                    });
                }
            }

            return LstCompras;
        }

        public List<Compras> ObterCompras(int ID)
        {
            List<Compras> res = new List<Compras>();
            Database db = ConnectionString;

            var result = db.Query("SELECT * FROM Compras where id_user ='" + ID + "';");

            while (result.Read())
            {
                res.Add(new Compras()
                {
                    ID = result["id_compra"],
                    Data = result["data_compra"],
                    IDProd = result["id_produto"],
                    NomeProd = result["nome_prod"],
                    Quantidade = result["quantidade"],
                    Preco = result["preco"],
                    ID_User = result["id_user"],
                });
            }

            db.Connection.Close();

            return res;

        }
        public List<Bar> ObterBar(int ID)
        {
            List<Bar> res = new List<Bar>();
            Database db = ConnectionString;

            var result = db.Query("SELECT * FROM compras_bar where id_user ='" + ID + "';");

            while (result.Read())
            {
                res.Add(new Bar()
                {
                    ID = result["id_compra_bar"],
                    Data = result["data_compra_bar"],
                    IDProd = result["id_bar"],
                    NomeProd = result["nome_bar"],
                    Preco = result["preco"],
                    ID_User = result["id_user"],
                });
            }

            db.Connection.Close();

            return res;

        }

        public List<Acessos> ObterAcessos(int ID)
        {
            List<Acessos> res = new List<Acessos>();
            Database db = ConnectionString;

            var result = db.Query("SELECT * FROM acessos where id_user ='" + ID + "';");

            while (result.Read())
            {
                res.Add(new Acessos()
                {
                    ID = result["id_acesso"],
                    ID_User = result["id_user"],
                    Data = result["data"],
                });
            }

            db.Connection.Close();

            return res;

        }

        /*public Compras ObterCompras(int ID)
        {
            Compras res = new Compras();

            using (Database db = ConnectionString)
            {
                using var result = db.Query("SELECT * FROM Compras where id_compra ='" + ID + "';");
                result.Read();
                if (result.Reader.HasRows)
                {
                    res = new Compras()
                    {
                        ID = result["id_compra"],
                        Data = result["data_compra "],
                        IDProd = result["id_prod"],
                        NomeProd = result["nome_prod"],
                        Quantidade = result["quantidade"],
                        Preco = result["ºpreco"],
                    };
                }

            }
            return res;
        }*/

    }
}
