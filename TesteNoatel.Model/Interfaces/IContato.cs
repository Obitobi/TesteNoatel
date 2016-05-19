using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteNoatel.Model.Interfaces
{
    public interface IContato
    {
        int Id { get; set; }
        int IdUser { get; set; }
        string Name { get; set; }
        string LastName { get; set; }
        string Phone { get; set; }
    }
}
