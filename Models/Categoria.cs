using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Models
{
    public class Categoria
    {
        public int categoriaId { get; set; }
        [Required (ErrorMessage = "Campo requerido")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre debe ser >3 && <50")]//define el minimo de caracteres
        [Display(Name = "Nombre categoria")]
        public string nombre { get; set; }

        [StringLength(256, ErrorMessage = "La descripcion no debe exceer de los 256 caracteres")]
        [Display (Name = "Descripción")]
        public string descripcion { get; set; }

        [Display(Name = "Estado categoria")]
        public Boolean estado { get; set; }
           

    }
}
