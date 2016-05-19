using SQLite;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteNoatel.Database;
using TesteNoatel.Model;
using TesteNoatel.Repositorio.Interfaces;

namespace TesteNoatel.Repositorio
{
    /// <summary>
    /// Repositório da aplicação
    /// </summary>
    public class Application : IApplication
    {
        private SQLiteConnection database;
        private static object locker = new object();

        /// <summary>
        /// Construtor do repositório Application
        /// </summary>
        /// <param name="filePath">Caminho do arquivos banco de dados</param>
        public Application(SQLiteConnection database)
        {
            this.database = database;
            this.database.CreateTable<DBUsuario>();
            this.database.CreateTable<DBContato>();
        }

        /// <summary>
        /// Método utilizado para salvar usuario (alterar ou inserir)
        /// </summary>
        /// <param name="usuario">Objeto usuario a ser utilizado</param>
        /// <returns>Retorna o usuario</returns>
        public Usuario SaveUser(Usuario usuario)
        {
            //lock (this)
            //{
            if (usuario.Id != 0)
            {
                database.Update(new DBUsuario(usuario));
                return usuario;
            }
            else
            {
                var user = new DBUsuario(usuario);
                var id = database.ExecuteScalar<int>("select last_insert_rowid()");
                user.Id = id+1;
                var value = database.Insert(user);
            }
            //}
            return User(usuario.Username, usuario.Password);
        }

        /// <summary>
        /// Método utilizado para pesquisar usuario pelo ID
        /// </summary>
        /// <param name="id">Id do usuario</param>
        /// <returns>Retorna o usuario</returns>
        public Usuario User(int id)
        {
            return new Usuario(database.Table<DBUsuario>().Where(x => x.Id == id).First());
        }

        /// <summary>
        /// Método utilizado para pesquisar usuario pelo nome de usuario e senha
        /// </summary>
        /// <param name="username">Nome de usuario</param>
        /// <param name="password">Senha</param>
        /// <returns>Retorna usuario</returns>
        public Usuario User(string username, string password)
        {
            var user = database.Table<DBUsuario>().Where(x => x.Username == username && x.Password == password).First();
            return new Usuario(user);
        }

        /// <summary>
        /// Método utilizado para verificar existência do username
        /// </summary>
        /// <param name="username">Username a ser testado</param>
        /// <returns>Retorna true para caso existe, false caso não</returns>
        public bool CheckUsernameExist(string username)
        {
            return database.Table<DBUsuario>().Any(x => x.Username == username);
        }

        /// <summary>
        /// Método utilizado para salvar contato(alterar, inserir)
        /// </summary>
        /// <param name="contato">Contato a ser utilizado</param>
        /// <returns>Retorna o contato preenchido</returns>
        public Contato SaveContact(Contato contato)
        {
            lock (this)
            {
                if (contato.Id != 0)
                {
                    database.Update(new DBContato(contato));
                    return contato;
                }
                else
                {
                    var contact = new DBContato(contato);
                    var id = database.ExecuteScalar<int>("select last_insert_rowid()");
                    contact.Id = id + 1;
                    var s = database.Insert(contact);
                }
            }
            return Contact(contato.Name, contato.LastName).Last();
        }

        /// <summary>
        /// Método utilizado para encontrar contato pelo nome e sobrenome
        /// </summary>
        /// <param name="name">Nome do contato</param>
        /// <param name="lastname">Sobrenome</param>
        /// <returns>Retorna array de contatos</returns>
        public Contato[] Contact(string name = "", string lastname = "")
        {
            var listContact = new List<Contato>();
            foreach (var item in database.Table<DBContato>().Where(x => x.Name.Contains(name) && x.LastName.Contains(lastname)))
            {
                listContact.Add(new Contato(item));
            }
            return listContact.ToArray();
        }

        /// <summary>
        /// Método utilizado para encontrar contato pelo nome, sobrenome e telefone
        /// </summary>
        /// <param name="name">Nome do contato</param>
        /// <param name="lastname">Sobrenome</param>
        /// <param name="phone">Telefone</param>
        /// <returns>Retorna contato</returns>
        public Contato[] Contact(string name = "", string lastname = "", string phone = "")
        {
            var listContact = new List<Contato>();
            TableQuery<DBContato> contacts = null;
            if (!string.IsNullOrEmpty(name))
            {
                contacts = database.Table<DBContato>().Where(x => x.Name.Contains(name));
            }
            else if (!string.IsNullOrEmpty(lastname))
            {
                contacts = database.Table<DBContato>().Where(x => x.LastName.Contains(lastname));
            }
            else if (!string.IsNullOrEmpty(phone))
            {
                contacts = database.Table<DBContato>().Where(x => x.Phone.Contains(phone));
            }

            foreach (var item in contacts)
            {
                listContact.Add(new Contato(item));
            }
            return listContact.ToArray();
        }

        /// <summary>
        /// Método utilizado para retornar contatos pelo id do usuario
        /// </summary>
        /// <param name="idUser">Id do usuario a ser pesquisado</param>
        /// <returns>Retorna array de contatos</returns>
        public Contato[] Contacts(int idUser)
        {
            var listContact = new List<Contato>();
            foreach (var item in database.Table<DBContato>().Where(x => x.IdUser == idUser))
            {
                listContact.Add(new Contato(item));
            }
            return listContact.ToArray();
        }
    }
}
