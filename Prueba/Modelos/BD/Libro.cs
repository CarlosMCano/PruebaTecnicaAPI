using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Prueba.Modelos
{
    [Table("Libros")]
    public class Libro
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descripcion { get; set; }
        public int PageCount { get; set; }
        public string Excerpt { get; set; }
        public DateTime PublishDate { get; set; }

    }
}
