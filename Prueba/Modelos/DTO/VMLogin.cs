using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.Modelos.DTO
{
    public class VMLogin
    {
        public VMLogin(string tokenString)
        {
            //Usuario = new Usuario(UserId, UserMail);
            //Perfil = new Perfil(PerfilId, PerfilDescripcion);
            Token = tokenString;
        }

        public string Token { get; set; }
    }
}
