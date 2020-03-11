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
    public class AcessoDados
    {
        private string StringDeConexao
        {
            get
            {
                //String para MySQL Server
                //TODO - 1 - mudar o nome da database e senha
                return "Server=10.27.47.51;Database=projeton10base;Uid=admin;Pwd=123";
            }
        }

        internal void Executar(string nomeProcedure, List<MySqlParameter> parametros)
        {
            MySqlCommand comando = new MySqlCommand();
            MySqlConnection conexao = new MySqlConnection(StringDeConexao);
            comando.Connection = conexao;
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = nomeProcedure;
            if (parametros != null)
                foreach (var item in parametros)
                    comando.Parameters.Add(item);

            try
            {
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexao.Close();
            }
        }

        internal DataSet Consultar(string nomeProcedure, List<MySqlParameter> parametros)
        {
            MySqlCommand comando = new MySqlCommand();
            MySqlConnection conexao = new MySqlConnection(StringDeConexao);

            comando.Connection = conexao;
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = nomeProcedure;
            if (parametros != null)
                foreach (var item in parametros)
                    comando.Parameters.Add(item);


            MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
            DataSet ds = new DataSet();

            try
            {
                conexao.Open();
                adapter.Fill(ds);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexao.Close();
            }

            return ds;
        }

    }
}
