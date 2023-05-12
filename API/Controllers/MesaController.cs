using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {
        [HttpGet]
        [Route("/[controller]")]

        public IActionResult Get(
            [FromServices] AppDbContext context)
        {
            return Ok(context.Mesas!.ToListAsync().Result);
        }

        [HttpGet("/[controller]/{id:int}")]

        public IActionResult GetById([FromRoute] int id,
                                    [FromServices] AppDbContext context)
        {
            return Ok(context.Mesas!.FirstOrDefaultAsync(e => e.MesaID == id).Result);
        }

        [HttpPost("/[controller]")]

        public IActionResult Post([FromBody] MesaModel mesaModel,
                             [FromServices] AppDbContext context)
        {
            context.Mesas!.Add(mesaModel);
            context.SaveChanges();
            return Created($"/{mesaModel.MesaID}", mesaModel);
        }

        [HttpPut("/[controller]/{id:int}")]

        public IActionResult Put([FromRoute] int id,
                            [FromBody] MesaModel mesaModel,
                            [FromServices] AppDbContext context)
        {
            var MesaModel = context.Mesas!.FirstOrDefaultAsync(e => e.MesaID == id).Result;
            
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

        [HttpDelete("/[controller]/{id:int}")]

        public IActionResult Delete([FromRoute] int id,
                            [FromServices] AppDbContext context)
        {
            var MesaModel = context.Mesas!.FirstOrDefaultAsync(e => e.MesaID == id).Result;
            
            if(MesaModel == null) {
                return NotFound();
            }

            context.Mesas!.Remove(MesaModel);
            context.SaveChanges();
            return Ok(MesaModel);
        }
    }
}