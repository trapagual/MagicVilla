using MagicVilla_API.Modelos.DTOs;

namespace MagicVilla_API.Datos
{
    public static class VillaStore
    {
        public static List<VillaDto> villasList = new List<VillaDto>
        {
            new VillaDto(1, "Vista a la montaña", 3, 50),
            new VillaDto(2, "Vista al chiringuito", 4, 80),
            new VillaDto(3, "Ostentosa", 6, 120),
            new VillaDto(4, "Baratuja", 2, 30)
        };
    }
}
