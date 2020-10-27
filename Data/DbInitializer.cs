using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sistema.Models;

namespace Sistema.Data
{
    public class DbInitializer
    {
        public static void Initilize(SistemaContext context)
        {
            //esta instruccion crea automaticamente la base de datos
            context.Database.EnsureCreated();

            //busca si existen registros en la tabla categorias
            if (context.Categoria.Any())
            {
                return;
            }

        }
    }
}
