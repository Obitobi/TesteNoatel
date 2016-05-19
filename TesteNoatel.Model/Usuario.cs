using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteNoatel.Model.Interfaces;

namespace TesteNoatel.Model
{
    public class Usuario : IUsuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Usuario()
        {

        }

        public Usuario(IUsuario sourceObject)
        {
            Id = sourceObject.Id;
            Username = sourceObject.Username;
            Password = sourceObject.Password;
        }
    }
}
