using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace ProjetoN9Aula.Classes
{
    public class CEP
    {

        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string EstadoUF { get; set; }

        public static CEP BuscarCEP(string cep)
        {
            CEP novoCep = new CEP();
            try
            {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://viacep.com.br/ws/" + cep + "/json/");
            request.AllowAutoRedirect = false;
            HttpWebResponse ChecaServidor = (HttpWebResponse)request.GetResponse();

            if (ChecaServidor.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Servidor indisponível");
            }

            using (Stream webStream = ChecaServidor.GetResponseStream())
            {
                if (webStream != null)
                {
                    using (StreamReader responseReader = new StreamReader(webStream))
                    {
                        string response = responseReader.ReadToEnd();
                        response = Regex.Replace(response, "[{},]", string.Empty);
                        response = response.Replace("\"", "");

                        String[] substrings = response.Split('\n');

                        int cont = 0;
                        foreach (var substring in substrings)
                        {
                            if (cont == 1)
                            {
                                string[] valor = substring.Split(":".ToCharArray());
                                if (valor[0] == "  erro")
                                {
                                    throw new Exception("CEP não encontrado");
                                }
                            }

                            //Logradouro
                            if (cont == 2)
                            {
                                string[] valor = substring.Split(":".ToCharArray());
                                    novoCep.Logradouro = valor[1].TrimStart();
                            }

                            //Complemento
                            if (cont == 3)
                            {
                                string[] valor = substring.Split(":".ToCharArray());
                                    novoCep.Complemento = valor[1].TrimStart();
                            }

                            //Bairro
                            if (cont == 4)
                            {
                                string[] valor = substring.Split(":".ToCharArray());
                                    novoCep.Bairro = valor[1].TrimStart();
                            }

                            //Localidade (Cidade)
                            if (cont == 5)
                            {
                                string[] valor = substring.Split(":".ToCharArray());
                                    novoCep.Cidade = valor[1].TrimStart();
                            }

                            //Estado (UF)
                            if (cont == 6)
                            {
                                string[] valor = substring.Split(":".ToCharArray());
                                    novoCep.EstadoUF = valor[1].TrimStart();
                            }

                            cont++;
                        }
                    }
                }
            }
                return novoCep;
            }
            catch (Exception)
            {

                throw;
            }
        }
           
    }
}