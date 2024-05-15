using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Modelos
{
    public class Villa(int Id, string Nombre, string detalle, string imagenurl, string amenidad, DateTime fechaCreacion, DateTime fechaActualizacion)
    {
        [Key] public int Id { get; set; } = Id;
        public string Nombre { get; set; } = Nombre;
        public string Detalle { get; set; } = detalle;
        [Required] public double Tarifa { get; set; }
        public int Ocupantes { get; set; }
        public int Metros2 { get; set; }
        public string ImagenURL { get; set; } = imagenurl;
        public string Amenidad { get; set; } = amenidad;
        public DateTime FechaCreacion { get; set; } = fechaCreacion;
        public DateTime FechaActualizacion { get; set; } = fechaActualizacion;
    }
}
