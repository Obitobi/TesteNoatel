using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteNoatel.Model;
using TesteNoatel.Model.Interfaces;

namespace TesteNoatel.Repositorio.Interfaces
{
    public interface IApplication
    {
        Usuario SaveUser(Usuario usuario);
        Usuario User(int id);
        Usuario User(string username, string password);
        bool CheckUsernameExist(string username);
        Contato SaveContact(Contato contato);
        Contato[] Contact(string name, string lastname);
        Contato[] Contact(string name, string lastname, string phone);
        Contato[] Contacts(int idUser);
    }
}
