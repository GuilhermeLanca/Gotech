using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Gotech.Activities
{
    [Activity()]
    public class LoginActivity : AppCompatActivity
    {
        CoordinatorLayout rootViewLogin;
        EditText edtLogin, edtSenha;
        Button btnEntrar;
        TextView txtCliqueCadastro;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_login);

            rootViewLogin = FindViewById<CoordinatorLayout>(Resource.Id.rootViewLogin);
            edtLogin = FindViewById<EditText>(Resource.Id.edtLogin);
            edtSenha = FindViewById<EditText>(Resource.Id.edtSenha);
            btnEntrar = FindViewById<Button>(Resource.Id.btnEntrar);
            txtCliqueCadastro = FindViewById<TextView>(Resource.Id.txtCliqueCadastro);

            btnEntrar.Click += BtnEntrar_Click;
        }

        private void BtnEntrar_Click(object sender, EventArgs e)
        {
            
        }
    }
}