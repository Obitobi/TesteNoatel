
using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using TesteNoatel.Model;
using SQLite;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using Android.Graphics;

namespace TesteNoatel.Droid
{
    /// <summary>
    /// Activity relacionada ao Login
    /// </summary>
    [Activity(MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/AppTheme", NoHistory = true)]
    public class LoginActivity : AppCompatActivity
    {
        private Button btnCadastro;
        private Button btnAcessar;
        private EditText txtUsername;
        private EditText txtPassword;
        private LinearLayout linearLayoutBaseLogin;

        private Core Core { get; set; }

        /// <summary>
        /// Método utilizado ao criar a activity
        /// </summary>
        /// <param name="bundle"></param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Relaciona o layout login a activity
            SetContentView(Resource.Layout.Login);
            //this.ActionBar.SetTitle(Resource.String.TitleToolbar);
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (toolbar != null)
            {
                toolbar.SetTitle(Resource.String.TitleToolbar);
                toolbar.SetTitleTextColor(Color.White);
                SetSupportActionBar(toolbar);
            }

            Init();

            btnCadastro.Click += delegate
            {
                bool status = false;
                try
                {
                    status = Core.CheckUsernameExists(txtUsername.Text);
                }
                catch
                {
                }
                if (status)
                {
                    Toast.MakeText(ApplicationContext, "Nome de usuario ja existe!", ToastLength.Long).Show();
                }
                else
                {
                    Core.Usuario = Core.SaveUser(txtUsername.Text, txtPassword.Text);
                    Toast.MakeText(ApplicationContext, "Usuário cadastrado!", ToastLength.Long).Show();
                    //Acessar();
                }
            };

            btnAcessar.Click += delegate
            {
                Acessar();
            };
        }

        private void Init()
        {
            btnCadastro = FindViewById<Button>(Resource.Id.lblCadastro);
            btnAcessar = FindViewById<Button>(Resource.Id.btnLogin);
            txtUsername = FindViewById<EditText>(Resource.Id.txtUsername);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            linearLayoutBaseLogin = FindViewById<LinearLayout>(Resource.Id.linearLayoutBaseLogin);
            System.IO.Directory.CreateDirectory($"{GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments)}/TesteNoatel");
            Core = new Core(DBConnect($"{GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments)}/TesteNoatel/DB_TESTE_NOATEL.db3"));
        }

        /// <summary>
        /// Conexão com o banco de dados
        /// </summary>
        /// <param name="filePath">Caminho do arquivo</param>
        /// <returns>Retorna conexão SQLiteConnection</returns>
        public SQLiteConnection DBConnect(string filePath)
        {
            return new SQLiteConnection(new SQLitePlatformAndroid(), filePath);
        }

        /// <summary>
        /// Método utilizado para autenticar e passar para a activity de contato
        /// </summary>
        private void Acessar()
        {
            try
            {
                var user = Core.User(txtUsername.Text, txtPassword.Text);
                if (user != null)
                {
                    var intent = new Intent(ApplicationContext, typeof(ContatosActivity));
                    var userSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(user);
                    var repositorySerialized = Newtonsoft.Json.JsonConvert.SerializeObject(Core.repository);
                    intent.PutExtra("userSerialized", userSerialized);
                    intent.PutExtra("repositorySerialized", repositorySerialized);
                    StartActivity(intent);
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(ApplicationContext, "Usuário ou senha errados/inexistentes", ToastLength.Long).Show();
            }
        }
    }
}

