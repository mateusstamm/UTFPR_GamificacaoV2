using GerenRest.API.Data;
using GerenRest.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace GerenRest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MesaController : ControllerBase
    {
        [HttpGet]
        [Route("/")]

        public IActionResult Get(
            [FromServices] AppDbContext context)
        {
            return Ok(context.Mesas!.ToList());
        }

        [HttpGet("/{id:int}")]

        public IActionResult GetById([FromRoute] int id,
                                    [FromServices] AppDbContext context)
        {
            var mesaModel = context.Mesas!.FirstOrDefault(e => e.MesaID == id);
            if(mesaModel == null) {
                return NotFound();
            }
            return Ok(mesaModel);
        }

        [HttpPost("/")]

        public IActionResult Post([FromBody] MesaModel mesaModel,
                             [FromServices] AppDbContext context)
        {
            context.Mesas!.Add(mesaModel);
            context.SaveChanges();
            return Created($"/{mesaModel.MesaID}", mesaModel);
        }

        [HttpPut("/")]

        public IActionResult Put([FromRoute] int id,
                            [FromBody] MesaModel mesaModel,
                            [FromServices] AppDbContext context)
        {
            var MesaModel = context.Mesas!.FirstOrDefault(e => e.MesaID == id);
            if(MesaModel == null) {
                return NotFound();
            }
            
            MesaModel.HoraAbertura = mesaModel.HoraAbertura;
            MesaModel.Numero = mesaModel.Numero;
            MesaModel.Ocupada = mesaModel.Ocupada;

            context.Mesas!.Update(MesaModel);
            context.SaveChanges();
            return Ok(MesaModel);
        }

        [HttpDelete("/")]

        public IActionResult Delete([FromRoute] int id,
                            [FromServices] AppDbContext context)
        {
            var MesaModel = context.Mesas!.FirstOrDefault(e => e.MesaID == id);
            if(MesaModel == null) {
                return NotFound();
            }

            context.Mesas!.Remove(MesaModel);
            context.SaveChanges();
            return Ok(MesaModel);
        }
    }
}