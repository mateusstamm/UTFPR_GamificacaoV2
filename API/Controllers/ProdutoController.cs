using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProdutoController : ControllerBase
    {
        [HttpGet]
        [Route("/[controller]")]

        public IActionResult Get(
            [FromServices] AppDbContext context)
        {
            return Ok(context.Produtos!
                            .Include(e => e.Categoria)
                            .ToListAsync()
                            .Result);
        }

        [HttpGet("/[controller]/{id:int}")]

        public IActionResult GetById([FromRoute] int id,
                                    [FromServices] AppDbContext context)
        {
            return Ok(context.Produtos!.Include(e => e.Categoria).FirstOrDefaultAsync(e => e.ProdutoID == id).Result);
        }

        [HttpPost("/[controller]")]

        public IActionResult Post([FromBody] ProdutoModel prodModel,
                             [FromServices] AppDbContext context)
        {
            
            context.Produtos!.Add(prodModel);
            context.SaveChanges();
            return Created($"/{prodModel.ProdutoID}", prodModel);
        }

        [HttpPut("/[controller]/{id:int}")]

        public IActionResult Put([FromRoute] int id,
                            [FromBody] ProdutoModel prodModel,
                            [FromServices] AppDbContext context)
        {
            var ProdModel = context.Produtos!.FirstOrDefaultAsync(e => e.ProdutoID == id).Result;
            
            if(ProdModel == null) {
                return NotFound();
            }
            
            ProdModel.CategoriaID = prodModel.CategoriaID;
            ProdModel.Categoria = prodModel.Categoria;
            ProdModel.Descricao = prodModel.Descricao;
            ProdModel.Nome = prodModel.Nome;
            ProdModel.Preco = prodModel.Preco;

            context.Produtos!.Update(ProdModel);
            context.SaveChanges();
            return Ok(ProdModel);
        }

        [HttpDelete("/[controller]/{id:int}")]

        public IActionResult Delete([FromRoute] int id,
                            [FromServices] AppDbContext context)
        {
            var ProdModel = context.Produtos!.FirstOrDefaultAsync(e => e.ProdutoID == id).Result;
            
            if(ProdModel == null) {
                return NotFound();
            }

            context.Produtos!.Remove(ProdModel);
            context.SaveChanges();
            return Ok(ProdModel);
        }
    }
}