using MagicVilla_API.Modelos.DTOs;

namespace MagicVilla_API.Datos
{
    public static class VillaStore
    {
        public static List<VillaDto> villasList = new List<VillaDto>
        {
            new VillaDto(1, "Vista a la montaña"),
            new VillaDto(2, "Vista al chiringuito")
        };
    }
}
