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
using Android.Support.V7.App;
using TesteNoatel.Model;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using Android.Graphics;

namespace TesteNoatel.Droid
{
    /// <summary>
    /// Classe utilizada pela parte de Contatos
    /// </summary>
    [Activity(Label = "ContatosActivity", Theme = "@style/AppTheme")]
    public class ContatosActivity : AppCompatActivity
    {
        private EditText txtNome, txtSobrenome, txtTelefone;
        private Button btnCadastrarContato;
        private TableLayout layoutContatos;
        private Core Core { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Contatos);

            Init();

            if (string.IsNullOrEmpty(txtNome.Text) && string.IsNullOrEmpty(txtSobrenome.Text) && string.IsNullOrEmpty(txtTelefone.Text))
            {
                PreencherContatos(0);
            }

            txtNome.TextChanged += delegate
            {
                PreencherContatos(1);
                if (string.IsNullOrEmpty(txtNome.Text) && string.IsNullOrEmpty(txtSobrenome.Text) && string.IsNullOrEmpty(txtTelefone.Text))
                {
                    PreencherContatos(0);
                }
            };

            txtSobrenome.TextChanged += delegate
            {
                PreencherContatos(1);
                if (string.IsNullOrEmpty(txtNome.Text) && string.IsNullOrEmpty(txtSobrenome.Text) && string.IsNullOrEmpty(txtTelefone.Text))
                {
                    PreencherContatos(0);
                }
            };

            txtTelefone.TextChanged += delegate
            {
                PreencherContatos(1);
                if (string.IsNullOrEmpty(txtNome.Text) && string.IsNullOrEmpty(txtSobrenome.Text) && string.IsNullOrEmpty(txtTelefone.Text))
                {
                    PreencherContatos(0);
                }
            };

            btnCadastrarContato.Click += delegate
            {
                try
                {
                    Core.SaveContact(Core.Usuario.Id, txtNome.Text, txtSobrenome.Text, txtTelefone.Text);
                    txtNome.Text = "";
                    txtSobrenome.Text = "";
                    txtTelefone.Text = "";
                    PreencherContatos(0);
                }
                catch (Exception ex)
                {
                    Toast.MakeText(ApplicationContext, "Preencha todos os campos por favor!", ToastLength.Long).Show();
                }
            };
            
        }

        /// <summary>
        /// Preenchimento da view dos contatos
        /// </summary>
        /// <param name="contatos">Array de contatos</param>
        public void PreencherView(Contato[] contatos)
        {
            layoutContatos.RemoveAllViews();
            var lp = new FrameLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent, GravityFlags.Center);
            layoutContatos.LayoutParameters = lp;
            //layoutContatos.StretchAllColumns = true;
            TableLayout.LayoutParams tableParams = new TableLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent, 2f);
            TableRow.LayoutParams rowParams = new TableRow.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent, 2f);
            foreach (var contato in contatos)
            {
                var layoutContato = new TableRow(ApplicationContext);
                var id = new TextView(ApplicationContext);
                var nome = new TextView(ApplicationContext);
                var sobrenome = new TextView(ApplicationContext);
                var telefone = new TextView(ApplicationContext);
                id.Text = contato.Id + "";
                //id.LayoutParameters = rowParams;
                nome.Text = contato.Name;
                nome.SetTextColor(Color.ParseColor("#000000"));
                nome.Gravity = GravityFlags.Left;
                nome.SetPadding(0, 0, 30, 10);
                //nome.LayoutParameters = rowParams;
                sobrenome.Text = contato.LastName;
                sobrenome.Gravity = GravityFlags.CenterHorizontal;
                sobrenome.SetTextColor(Color.ParseColor("#000000"));
                sobrenome.SetPadding(0, 0, 30, 10);
                //sobrenome.LayoutParameters = rowParams;
                telefone.Text = contato.Phone;
                telefone.Gravity = GravityFlags.Right;
                telefone.SetTextColor(Color.ParseColor("#000000"));
                telefone.SetPadding(0, 0, 0, 10);
                //telefone.LayoutParameters = rowParams;
                id.Visibility = ViewStates.Invisible;
                //layoutContato.LayoutParameters = rowParams;
                layoutContato.AddView(id, rowParams);
                layoutContato.AddView(nome, rowParams);
                layoutContato.AddView(sobrenome, rowParams);
                layoutContato.AddView(telefone, rowParams);
                layoutContatos.AddView(layoutContato, tableParams);
            }
        }

        /// <summary>
        /// Método utilizado para preencher a tabela de contatos
        /// </summary>
        /// <param name="status">Status ou tipo a ser preenchido</param>
        private void PreencherContatos(int status)
        {
            try
            {
                switch (status)
                {
                    case 0:
                        PreencherView(Core.Contacts(Core.Usuario.Id));
                        break;
                    case 1:
                        PreencherView(Core.Contacts(txtNome.Text, txtSobrenome.Text, txtTelefone.Text));
                        break;
                }
            }
            catch (Exception ex)
            {
                layoutContatos.RemoveAllViews();
                var oops = new TextView(ApplicationContext);
                oops.Text = "Oops, nenhum contato detectado";
                var row = new TableRow(ApplicationContext);
                row.AddView(oops);
                layoutContatos.AddView(row);
            }
        }

        /// <summary>
        /// Método utilizado para inicializar as entradas
        /// </summary>
        private void Init()
        {
            txtNome = FindViewById<EditText>(Resource.Id.txtNome);
            txtSobrenome = FindViewById<EditText>(Resource.Id.txtSobrenome);
            txtTelefone = FindViewById<EditText>(Resource.Id.txtTelefone);
            btnCadastrarContato = FindViewById<Button>(Resource.Id.btnCadastrarContato);
            layoutContatos = FindViewById<TableLayout>(Resource.Id.linearLayoutContatos);
            var valor = Intent.Extras.GetString("coreSerialized");
            Core = new Core(DBConnect($"{GetExternalFilesDir(Android.OS.Environment.DirectoryDocuments)}/TesteNoatel/DB_TESTE_NOATEL.db3"));
            Core.Usuario = Newtonsoft.Json.JsonConvert.DeserializeObject<Usuario>(Intent.Extras.GetString("userSerialized"));
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
    }
}