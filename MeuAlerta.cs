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

namespace ProjetoN9Aula.Classes
{
    public class MeuAlerta
    {
        public enum Botao{
            Positivo,
            Negativo,
            Neutro
        };

        public static void MsgAlerta(Context contexto, string titulo, string mensagem, Botao tipoBotao, string tituloBotao, Action acao)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(contexto);
            builder.SetTitle(titulo);
            builder.SetMessage(mensagem);
            builder.SetIconAttribute(Android.Resource.Attribute.AlertDialogIcon);
            if (tipoBotao == Botao.Positivo || tipoBotao == Botao.Negativo)
            {
                builder.SetPositiveButton(tituloBotao, delegate { acao(); });
            }
            else
            {
                builder.SetNeutralButton(tituloBotao, delegate { acao(); });
            }
            builder.Show();
        }

        /// <summary>
        /// Por favor, não coloque o estilo do segundo botão como Positivo.
        /// </summary>
        /// <param name="contexto"></param>
        /// <param name="titulo"></param>
        /// <param name="mensagem"></param>
        /// <param name="estiloSegundoBotao"></param>
        /// <param name="botaoPositivo"></param>
        /// <param name="segundoBotao"></param>
        public static void MsgAlerta(Context contexto, string titulo, string mensagem, Botao estiloSegundoBotao, Action botaoPositivo, Action segundoBotao)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(contexto);
            builder.SetTitle(titulo);
            builder.SetMessage(mensagem);
            builder.SetIconAttribute(Android.Resource.Attribute.AlertDialogIcon);
            builder.SetPositiveButton("OK", delegate { botaoPositivo(); });
            if (estiloSegundoBotao == Botao.Negativo)
            {
                builder.SetNegativeButton("CANCELAR", delegate { segundoBotao(); });
            }
            else
            {
                builder.SetNeutralButton("MAIS TARDE", delegate { segundoBotao(); });
            }
            
            builder.Show();
        }

        public static void MsgAlerta(Context contexto, string titulo, string mensagem, Action botaoPositivo, Action botaoNegativo, Action botaoNeutro)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(contexto);
            builder.SetTitle(titulo);
            builder.SetMessage(mensagem);
            builder.SetIconAttribute(Android.Resource.Attribute.AlertDialogIcon);
            builder.SetPositiveButton("OK", delegate { botaoPositivo(); });
            builder.SetNegativeButton("CANCELAR", delegate { botaoNegativo(); });
            builder.SetNeutralButton("MAIS TARDE", delegate { botaoNeutro(); });
            builder.Show();
        }
    }
}