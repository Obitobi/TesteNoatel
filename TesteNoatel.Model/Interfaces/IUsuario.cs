﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteNoatel.Model.Interfaces
{
    public interface IUsuario
    {
        int Id { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}
