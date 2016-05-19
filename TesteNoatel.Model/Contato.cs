using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteNoatel.Model.Interfaces;

namespace TesteNoatel.Model
{
    public class Contato : IContato
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set;}

        public Contato()
        {

        }

        public Contato(IContato sourceObject)
        {
            Id = sourceObject.Id;
            IdUser = sourceObject.IdUser;
            Name = sourceObject.Name;
            LastName = sourceObject.LastName;
            Phone = sourceObject.Phone;
        }
    }
}
