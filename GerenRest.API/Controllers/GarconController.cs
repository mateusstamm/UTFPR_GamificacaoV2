using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarconController : ControllerBase
    {
        [HttpGet]
        [Route("/[controller]")]

        public IActionResult Get(
            [FromServices] AppDbContext context)
        {
            return Ok(context.Garcons!.ToListAsync().Result);
        }

        [HttpGet("/[controller]/{id:int}")]

        public IActionResult GetById([FromRoute] int id,
                                    [FromServices] AppDbContext context)
        {
            return Ok(context.Garcons!.FirstOrDefaultAsync(e => e.GarconID == id).Result);
        }

        [HttpPost("/[controller]")]

        public IActionResult Post([FromBody] GarconModel garModel,
                             [FromServices] AppDbContext context)
        {
            context.Garcons!.Add(garModel);
            context.SaveChanges();
            return Created($"/{garModel.GarconID}", garModel);
        }

        [HttpPut("/[controller]/{id:int}")]

        public IActionResult Put([FromRoute] int id,
                            [FromBody] GarconModel garModel,
                            [FromServices] AppDbContext context)
        {
            var GarModel = context.Garcons!.FirstOrDefaultAsync(e => e.GarconID == id).Result;

            if(GarModel == null) {
                return NotFound();
            }
            
            GarModel.Nome = garModel.Nome;
            GarModel.Sobrenome = garModel.Sobrenome;
            GarModel.NumIdentificao = garModel.NumIdentificao;
            GarModel.Telefone = garModel.Telefone;
            
            context.Garcons!.Update(GarModel);
            context.SaveChanges();
            return Ok(GarModel);
        }

        [HttpDelete("/[controller]/{id:int}")]

        public IActionResult Delete([FromRoute] int id,
                            [FromServices] AppDbContext context)
        {
            var GarconModel = context.Garcons!.FirstOrDefaultAsync(e => e.GarconID == id).Result!;
            
            if(GarconModel == null) {
                return NotFound();
            }

            context.Garcons!.Remove(GarconModel);
            context.SaveChanges();
            return Ok(GarconModel);
        }

    }
}