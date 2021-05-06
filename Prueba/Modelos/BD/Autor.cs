using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.Modelos
{

    [Table("Autores")]
    public class Autor : Base
    {

        [ForeignKey("libro")]
        public int IdBook { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual Libro libro { get; set; }
    }
}
