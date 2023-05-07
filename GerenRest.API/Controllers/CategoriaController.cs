using GerenRest.API.Data;
using GerenRest.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace GerenRest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoriaController : ControllerBase
    {
        [HttpGet("/[controller]")]

        public IActionResult Get([FromServices] AppDbContext context)
        {
            return Ok(context.Categorias!.ToList());
        }

        [HttpGet("/[controller]/{id:int}")]

        public IActionResult GetById([FromRoute] int id,
                                    [FromServices] AppDbContext context)
        {
            var categoriaModel = context.Categorias!.FirstOrDefault(e => e.CategoriaID == id);
            if(categoriaModel == null) {
                return NotFound();
            }
            return Ok(categoriaModel);
        }

        [HttpPost("/[controller]")]

        public IActionResult Post([FromBody] CategoriaModel catModel,
                             [FromServices] AppDbContext context)
        {
            context.Categorias!.Add(catModel);
            context.SaveChanges();
            return Created($"/{catModel.CategoriaID}", catModel);
        }

        [HttpPut("/[controller]")]

        public IActionResult Put([FromRoute] int id,
                            [FromBody] CategoriaModel catModel,
                            [FromServices] AppDbContext context)
        {
            var CatModel = context.Categorias!.FirstOrDefault(e => e.CategoriaID == id);
            if(CatModel == null) {
                return NotFound();
            }
            
            CatModel.Descricao = catModel.Descricao;
            CatModel.Nome = catModel.Nome;

            context.Categorias!.Update(CatModel);
            context.SaveChanges();
            return Ok(CatModel);
        }

        [HttpDelete("/[controller]")]

        public IActionResult Delete([FromRoute] int id,
                            [FromServices] AppDbContext context)
        {
            var CatModel = context.Categorias!.FirstOrDefault(e => e.CategoriaID == id);
            if(CatModel == null) {
                return NotFound();
            }

            context.Categorias!.Remove(CatModel);
            context.SaveChanges();
            return Ok(CatModel);
        }

    }
}