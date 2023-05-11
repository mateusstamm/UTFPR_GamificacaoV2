using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoriaController : ControllerBase
    {
        [HttpGet("/[controller]")]

        public IActionResult Get([FromServices] AppDbContext context)
        {
            return Ok(context.Categorias!.ToListAsync().Result);
        }

        [HttpGet("/[controller]/{id:int}")]

        public IActionResult GetById([FromRoute] int id,
                                    [FromServices] AppDbContext context)
        {
            return Ok(context.Categorias!.FirstOrDefaultAsync(e => e.CategoriaID == id).Result);
        }

        [HttpPost("/[controller]")]

        public IActionResult Post([FromBody] CategoriaModel catModel,
                             [FromServices] AppDbContext context)
        {
            context.Categorias!.Add(catModel);
            context.SaveChanges();
            return Created($"/{catModel.CategoriaID}", catModel);
        }

        [HttpPut("/[controller]/{id:int}")]

        public IActionResult Put([FromRoute] int id,
                            [FromBody] CategoriaModel catModel,
                            [FromServices] AppDbContext context)
        {
            var CatModel = context.Categorias!.FirstOrDefaultAsync(e => e.CategoriaID == id).Result;

            if(CatModel == null) {
                return NotFound();
            }
            
            CatModel.Nome = catModel.Nome;
            CatModel.Descricao = catModel.Descricao;

            context.Categorias!.Update(CatModel);
            context.SaveChanges();
            return Ok(CatModel);
        }

        [HttpDelete("/[controller]/{id:int}")]

        public IActionResult Delete([FromRoute] int id,
                            [FromServices] AppDbContext context)
        {
            var CatModel = context.Categorias!.FirstOrDefaultAsync(e => e.CategoriaID == id).Result!;
            
            if(CatModel == null) {
                return NotFound();
            }

            context.Categorias!.Remove(CatModel);
            context.SaveChanges();
            return Ok(CatModel);
        }

    }
}