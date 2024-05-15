using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Modelos.DTOs
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }
        public int Ocupantes { get; set; }
        public int Metros2 { get; set; }
        public string Detalle { get; set; }
        [Required] public double Tarifa { get; set; }
        public string ImagenURL { get; set; }
        public string Amenidad { get; set; }




        public VillaDto(int id, string nombre, int ocupantes, int metros2, string detalle, double tarifa, string imagenURL, string amenidad)
        {
            Id = id;
            Nombre = nombre ?? throw new ArgumentNullException(nameof(nombre));
            Ocupantes = ocupantes;
            Metros2 = metros2;
            Detalle = detalle;
            Tarifa = tarifa;
            ImagenURL = imagenURL;
            Amenidad = amenidad;
        }
    }
}
