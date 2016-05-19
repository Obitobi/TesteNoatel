using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteNoatel.Model.Interfaces;

namespace TesteNoatel.Database
{
    [Table("USERS")]
    public class DBUsuario : IUsuario
    {
        [PrimaryKey, Column("ID")]
        public int Id { get; set; }
        [Column("USERNAME")]
        public string Username { get; set; }
        [Column("PASSWORD")]
        public string Password { get; set; }

        public DBUsuario()
        {

        }

        public DBUsuario(IUsuario sourceObject)
        {
            Id = sourceObject.Id;
            Username = sourceObject.Username;
            Password = sourceObject.Password;
        }
    }
}
