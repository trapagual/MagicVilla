using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Modelos.DTOs
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }


        public VillaDto(int Id, string Nombre)
        {
            this.Id = Id;
            this.Nombre  = Nombre;
        }


    }
}
