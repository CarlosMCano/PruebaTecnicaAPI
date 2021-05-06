using Prueba.Modelos.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace Prueba
{
    public class Utilidad
    {
        public Utilidad()
        {
        }

        internal static string GenerarClave(string clave)
        {
            byte[] hashValue;
            //Create a new instance of the UnicodeEncoding class to 
            //convert the string into an array of Unicode bytes.
            UnicodeEncoding ue = new UnicodeEncoding();
            //Convert the string into an array of bytes.
            byte[] messageBytes = ue.GetBytes(clave);
            //Create a new instance of the SHA1Managed class to create 
            //the hash value.
            SHA1Managed shHash = new SHA1Managed();
            //Create the hash value from the array of bytes.
            hashValue = shHash.ComputeHash(messageBytes);
            //Display the hash value to the console. 
            string a = "";
            foreach (byte b in hashValue)
            {
                a = a + "" + b;
            }
            return a;
        }

        internal static Login UsuarioLogin(ClaimsIdentity dato)
        {
            Login valor = new Login();
            valor.Guid = "";

            try
            {
                IEnumerable<Claim> claims = dato.Claims;
                //usuario ID
                if (!string.IsNullOrEmpty(claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value))
                {
                    valor.Guid = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                }

                if (!string.IsNullOrEmpty(claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value))
                {
                    valor.Mail = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                }

                //roles
                foreach (Claim claim in claims.Where(c => c.Type == ClaimTypes.Role))
                {
                    valor.Perfiles.Add(claim.Value);
                }

                return valor;
            }
            catch (Exception)
            {
                return valor;
            }

        }

        internal static Boolean EmailBienEscrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
