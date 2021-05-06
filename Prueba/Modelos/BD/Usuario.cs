using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.Modelos.BD
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        public string Clave { get; set; }

        public string Correo { get; set; }

        public string Estado { get; set; } = "1";

        public string Bloqueo { get; set; } = "0";
    }
}
