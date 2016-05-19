using SQLite;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteNoatel.Model;
using TesteNoatel.Repositorio;

namespace TesteNoatel
{
    /// <summary>
    /// Classe para ser utilizada pelo projeto inteiro para conexão com repositório
    /// </summary>
    public class Core
    {

        /// <summary>
        /// Repositório
        /// </summary>
        public Application repository { get; set; }

        public Usuario Usuario { get; set; }

        /// <summary>
        /// Método construtor utilizado para instanciar a propriedade repository
        /// </summary>
        /// <param name="filePathDatabase">Caminho do arquivo</param>
        public Core(SQLiteConnection database)
        {
            repository = new Application(database);
        }
        
        /// <summary>
        /// Método utilizado para salvar usuario
        /// </summary>
        /// <param name="username">Nome de usuario</param>
        /// <param name="password">Senha</param>
        /// <returns>Retorna usuario</returns>
        public Usuario SaveUser(string username, string password)
        {
            return repository.SaveUser(new Usuario
            {
                Username = username,
                Password = password
            });
        }

        /// <summary>
        /// Método utilizado para autenticar usuario
        /// </summary>
        /// <param name="username">Nome do usuario</param>
        /// <param name="password">Senha</param>
        /// <returns>Retorna o usuario</returns>
        public Usuario User(string username, string password)
        {
            return repository.User(username, password);
        }

        /// <summary>
        /// Verifica se o username existe
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>Retorna true se existe false se não</returns>
        public bool CheckUsernameExists(string username)
        {
            return repository.CheckUsernameExist(username);
        }

        /// <summary>
        /// Método para salvar contato
        /// </summary>
        /// <param name="idUser">Id do usuario</param>
        /// <param name="name">Nome do contato</param>
        /// <param name="lastname">Sobrenome do contato</param>
        /// <param name="phone">Telefone</param>
        /// <returns>Contato</returns>
        public Contato SaveContact(int idUser, string name, string lastname, string phone)
        {
            return repository.SaveContact(new Contato
            {
                IdUser = idUser,
                Name = name,
                LastName = lastname,
                Phone = phone
            });
        }

        /// <summary>
        /// Contato a ser pesquisado
        /// </summary>
        /// <param name="name">Nome</param>
        /// <param name="lastname">Sobrenome</param>
        /// <returns>Retorna contato</returns>
        public Contato[] Contacts(string name, string lastname)
        {
            return repository.Contact(name, lastname);
        }

        /// <summary>
        /// Contato a partir do nome ou sobrenome ou telefone
        /// </summary>
        /// <param name="name">Nome</param>
        /// <param name="lastname">Sobrenome</param>
        /// <param name="phone">Telefone</param>
        /// <returns>Retorna contato</returns>
        public Contato[] Contacts(string name, string lastname, string phone)
        {
            return repository.Contact(name, lastname, phone);
        }

        /// <summary>
        /// Contatos a partir do id do usuario
        /// </summary>
        /// <param name="idUser">Id do usuario</param>
        /// <returns>Retorna array de contatos</returns>
        public Contato[] Contacts(int idUser)
        {
            return repository.Contacts(idUser);
        }
    }
}
