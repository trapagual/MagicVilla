using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;

        public VillaController(ILogger<VillaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            _logger.LogInformation("Listado de villas");
            return Ok(VillaStore.villasList);
        }

        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("El id no puede ser cero");
                return BadRequest();
            }

            var villa = VillaStore.villasList.FirstOrDefault(v => v.Id == id);

            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDto> CrearVilla(VillaDto villa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (villa == null)
            {
                return BadRequest(villa);
            }

            if (villa.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if (VillaStore.villasList.FirstOrDefault(v => v.Nombre.ToLower() == villa.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("YaExiste", "Ya existe una villa con ese nombre");
                return BadRequest(ModelState);
            }

            villa.Id = VillaStore.villasList.OrderByDescending(v => v.Id).First().Id + 1;
            VillaStore.villasList.Add(villa);
            return CreatedAtRoute("GetVilla", new { villa.Id }, villa);

        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteVilla(int id) {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.villasList.FirstOrDefault(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            VillaStore.villasList.Remove(villa);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villa)
        {
            if (villa == null || id != villa.Id) {
                return BadRequest();
            }

            var b = VillaStore.villasList.FirstOrDefault(v => v.Id == id);
            if (b == null)
            {
                return NotFound();
            }

            b.Nombre = villa.Nombre;
            b.Ocupantes = villa.Ocupantes;
            b.Metros2 = villa.Metros2;

            return NoContent();
        }

        /*
         * OJO OJO OJO OJO OJO OJO OJO OJO OJO OJO OJO OJO OJO OJO OJO OJO OJO OJO
         *      ESTE METODO NO FUNCIONA. PROBABLEMENTE PORQUE EL NUGET NewtonsoftJson que utiliza en el video,
         *      no se ha podido cargar aqui. Aparentemente todo va bien, devuelve un NoContent, pero no actualiza
         *      el registro.
         *  OJO OJO OJO OJO OJO OJO OJO OJO OJO
         */
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var b = VillaStore.villasList.FirstOrDefault(v => v.Id == id);
            if (b == null)
            {
                return NotFound();
            }

            patchDto.ApplyTo(b);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}
