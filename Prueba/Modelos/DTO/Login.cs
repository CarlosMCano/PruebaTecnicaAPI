using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.Modelos.DTO
{
    public class Login
    {
        public Login()
        {
            Perfiles = new List<string>();
        }
        public string Guid { get; set; }
        public string Mail { get; set; }

        public List<string> Perfiles { get; set; }
    }
}
