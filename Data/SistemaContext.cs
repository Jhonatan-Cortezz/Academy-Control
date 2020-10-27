using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sistema.Models;

namespace Sistema.Data
{
    //esta se crea automaticamente cuado añadi el controlador categorias y cree un nuevo contexto de datos
    public class SistemaContext : DbContext
    {
        public SistemaContext (DbContextOptions<SistemaContext> options)
            : base(options)
        {
        }

        //aca debere incluir todas las entidades(Modelos) a utilizar
        public DbSet<Sistema.Models.Categoria> Categoria { get; set; }//para la clase Categoria
    }
}
