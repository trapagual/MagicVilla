using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            return Ok(VillaStore.villasList);
        }

        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if(id == 0)
            {
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
            if(!ModelState.IsValid)
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
            
            if (VillaStore.villasList.FirstOrDefault(v=>v.Nombre.ToLower() == villa.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("YaExiste", "Ya existe una villa con ese nombre");
                return BadRequest(ModelState);
            }

            villa.Id = VillaStore.villasList.OrderByDescending(v => v.Id).First().Id + 1;
            VillaStore.villasList.Add(villa);
            return CreatedAtRoute("GetVilla", new { villa.Id }, villa);

        }
    }
}
