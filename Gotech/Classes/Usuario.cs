using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;

namespace Gotech.Classes
{
    public class Usuario : AcessoDados
    {
        #region Propriedades

        public int Id_Usuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Frase { get; set; }
        public string Fone_Res { get; set; }
        public string Fone_Com { get; set; }
        public string Fone_Alternativo { get; set; }
        public DateTime Data_Nasc { get; set; }
        public string CPF_CNPJ { get; set; }
        public string CEP { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public int Tipo { get; set; }
        public string Foto { get; set; }
        public int Ativo { get; set; }
        public byte[] FotoProfileArquivo { get; set; }



        #endregion

        #region Construtores

        public Usuario()
        {

        }

        public Usuario(int id_Usuario, string nome, string email, string login, string senha, string frase, string fone_Res, string fone_Com, string fone_Alternativo, DateTime data_Nasc, string cPF_CNPJ, string cEP, string numero, string complemento, int tipo, string foto, int ativo, byte[] fotoProfileArquivo)
        {
            Id_Usuario = id_Usuario;
            Nome = nome;
            Email = email;
            Login = login;
            Senha = senha;
            Frase = frase;
            Fone_Res = fone_Res;
            Fone_Com = fone_Com;
            Fone_Alternativo = fone_Alternativo;
            Data_Nasc = data_Nasc;
            CPF_CNPJ = cPF_CNPJ;
            CEP = cEP;
            Numero = numero;
            Complemento = complemento;
            Tipo = tipo;
            Foto = foto;
            Ativo = ativo;
            FotoProfileArquivo = fotoProfileArquivo;
        }



        #endregion

        #region Métodos

        public List<Usuario> Converter(DataSet ds)
        {
            List<Usuario> usuariosConvertidas = new List<Usuario>();
            try
            {


                if (ds != null &&
                    ds.Tables != null &&
                    ds.Tables.Count > 0 &&
                    ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow linha in ds.Tables[0].Rows)
                    {
                        Usuario usuario = new Usuario
                        {
                            Id_Usuario = (int)ds.Tables[0].Rows[0][0],
                            Nome = ds.Tables[0].Rows[0][1].ToString(),
                            Email = ds.Tables[0].Rows[0][2].ToString(),
                            Login = ds.Tables[0].Rows[0][3].ToString(),
                            Senha = ds.Tables[0].Rows[0][4].ToString(),
                            Frase = ds.Tables[0].Rows[0][5].ToString(),
                            Fone_Res = ds.Tables[0].Rows[0][6].ToString(),
                            Fone_Com = ds.Tables[0].Rows[0][7].ToString(),
                            Fone_Alternativo = ds.Tables[0].Rows[0][8].ToString(),
                            Data_Nasc = Convert.ToDateTime(ds.Tables[0].Rows[0][9]),
                            CPF_CNPJ = ds.Tables[0].Rows[0][10].ToString(),
                            CEP = ds.Tables[0].Rows[0][11].ToString(),
                            Numero = ds.Tables[0].Rows[0][12].ToString(),
                            Complemento = ds.Tables[0].Rows[0][13].ToString(),
                            Tipo = (int)ds.Tables[0].Rows[0][14],
                            Foto = ds.Tables[0].Rows[0][15].ToString(),
                            Ativo = (int)ds.Tables[0].Rows[0][16]
                            //FotoProfileArquivo = (byte[])ds.Tables[0].Rows[0][9]
                        };
                        usuariosConvertidas.Add(usuario);
                    }
                }
                return usuariosConvertidas;
            }
            catch(ApplicationException)
            {
                throw new Exception("Usuário bloqueado ou inexistente ou Login e/ou senha não conferem.");
            }
            catch (Exception)
            {
                throw;
            }
        }


        public Usuario RealizarLogin(string login, string senha)
        {
            List<MySqlParameter> parametros = new List<MySqlParameter>
            {
                new MySqlParameter("Vlogin", login)
            };

            try
            {
                DataSet ds = Consultar("SP_LoginUsuario", parametros);

                Usuario pessoaLogada = Converter(ds)[0];

                if (pessoaLogada.Ativo == 1)
                {
                    if (pessoaLogada.Senha == Crypto.sha256encrypt(senha))
                    {
                        return pessoaLogada;
                    }
                    else
                    {
                        throw new Exception("Usuário bloqueado ou inexistente ou Login e/ou senha não conferem.");
                    }
                }
                else
                {
                    throw new Exception("Usuário bloqueado ou inexistente ou Login e/ou senha não conferem.");
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}