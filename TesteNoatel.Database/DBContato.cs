using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteNoatel.Model.Interfaces;

namespace TesteNoatel.Database
{
    [Table("CONTACTS")]
    public class DBContato : IContato
    {
        [PrimaryKey, Column("ID")]
        public int Id { get; set; }
        [Column("ID_USER"), Indexed]
        public int IdUser { get; set; }
        [Column("NAME")]
        public string Name { get; set; }
        [Column("LASTNAME")]
        public string LastName { get; set; }
        [Column("PHONE")]
        public string Phone { get; set; }

        public DBContato()
        {

        }

        public DBContato(IContato sourceObject)
        {
            Id = sourceObject.Id;
            IdUser = sourceObject.IdUser;
            Name = sourceObject.Name;
            LastName = sourceObject.LastName;
            Phone = sourceObject.Phone;
        }
    }
}
