using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class Serie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Clave primaria

        [Required]
        public string Nombre { get; set; } // Nombre de la serie

        [Required]
        public string Portada { get; set; } // URL de la imagen de portada

        [Required]
        public string EnlaceVideo { get; set; } // Enlace al video (YouTube)

        // Relación con Productora
        [Required]
        public int ProductoraId { get; set; }
        [ForeignKey("ProductoraId")]

        public Productora Productora { get; set; } 

        // Relación con Género
        [Required]
        public int GeneroId { get; set; }
        [ForeignKey("GeneroId")]
        public Genero Genero { get; set; } 
    }
}
