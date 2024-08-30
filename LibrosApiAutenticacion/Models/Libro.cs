using System.ComponentModel.DataAnnotations;

namespace LibrosApiAutenticacion.Models
{
    public class Libro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; }
        public int AnioPublicacion { get; set; }
    }
}
