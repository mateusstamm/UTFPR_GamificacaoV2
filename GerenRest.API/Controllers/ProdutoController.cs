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
    public class ProdutoController : ControllerBase
    {
        [HttpGet]
        [Route("/")]

        public IActionResult Get(
            [FromServices] AppDbContext context)
        {
            return Ok(context.Produtos!.ToList());
        }

        [HttpGet("/{id:int}")]

        public IActionResult GetById([FromRoute] int id,
                                    [FromServices] AppDbContext context)
        {
            var produtoModel = context.Produtos!.FirstOrDefault(e => e.ProdutoID == id);
            if(produtoModel == null) {
                return NotFound();
            }
            return Ok(produtoModel);
        }

        [HttpPost("/")]

        public IActionResult Post([FromBody] ProdutoModel prodModel,
                             [FromServices] AppDbContext context)
        {
            context.Produtos!.Add(prodModel);
            context.SaveChanges();
            return Created($"/{prodModel.ProdutoID}", prodModel);
        }

        [HttpPut("/")]

        public IActionResult Put([FromRoute] int id,
                            [FromBody] ProdutoModel prodModel,
                            [FromServices] AppDbContext context)
        {
            var ProdModel = context.Produtos!.FirstOrDefault(e => e.ProdutoID == id);
            if(ProdModel == null) {
                return NotFound();
            }
            
            ProdModel.Categoria = prodModel.Categoria;
            ProdModel.Descricao = prodModel.Descricao;
            ProdModel.Nome = prodModel.Nome;
            ProdModel.Preco = prodModel.Preco;

            context.Produtos!.Update(ProdModel);
            context.SaveChanges();
            return Ok(ProdModel);
        }

        [HttpDelete("/")]

        public IActionResult Delete([FromRoute] int id,
                            [FromServices] AppDbContext context)
        {
            var ProdModel = context.Produtos!.FirstOrDefault(e => e.ProdutoID == id);
            if(ProdModel == null) {
                return NotFound();
            }

            context.Produtos!.Remove(ProdModel);
            context.SaveChanges();
            return Ok(ProdModel);
        }
    }
}