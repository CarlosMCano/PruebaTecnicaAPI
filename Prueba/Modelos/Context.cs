using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Prueba.Modelos.BD;

namespace Prueba.Modelos
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Autor> Autores { get; set; }
        public virtual DbSet<Libro> Libros { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
    }
}
