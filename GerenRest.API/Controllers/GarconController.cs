using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenRest.API.Data;
using GerenRest.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace GerenRest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GarconController : ControllerBase
    {
        [HttpGet]
        [Route("/")]

        public IActionResult Get(
            [FromServices] AppDbContext context)
        {
            return Ok(context.Garcons!.ToList());
        }

        [HttpGet("/{id:int}")]

        public IActionResult GetById([FromRoute] int id,
                                    [FromServices] AppDbContext context)
        {
            var garconModel = context.Garcons!.FirstOrDefault(e => e.GarconID == id);
            if(garconModel == null) {
                return NotFound();
            }
            return Ok(garconModel);
        }

        [HttpPost("/")]

        public IActionResult Post([FromBody] GarconModel garModel,
                             [FromServices] AppDbContext context)
        {
            context.Garcons!.Add(garModel);
            context.SaveChanges();
            return Created($"/{garModel.GarconID}", garModel);
        }

        [HttpPut("/")]

        public IActionResult Put([FromRoute] int id,
                            [FromBody] GarconModel garModel,
                            [FromServices] AppDbContext context)
        {
            var GarModel = context.Garcons!.FirstOrDefault(e => e.GarconID == id);
            if(GarModel == null) {
                return NotFound();
            }
            
            GarModel.GarconID = GarModel.GarconID;
            GarModel.Nome = GarModel.Nome;
            GarModel.Sobrenome = GarModel.Sobrenome;
            GarModel.NumIdentificao = GarModel.NumIdentificao;
            GarModel.Telefone = GarModel.Telefone;
            
            context.Garcons!.Update(GarModel);
            context.SaveChanges();
            return Ok(GarModel);
        }

        [HttpDelete("/")]

        public IActionResult Delete([FromRoute] int id,
                            [FromServices] AppDbContext context)
        {
            var GarconModel = context.Garcons!.FirstOrDefault(e => e.GarconID == id);
            if(GarconModel == null) {
                return NotFound();
            }

            context.Garcons!.Remove(GarconModel);
            context.SaveChanges();
            return Ok(GarconModel);
        }

    }
}