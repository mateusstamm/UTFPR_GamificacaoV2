using GerenRest.API.Data;
using GerenRest.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenRest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtendimentoController : ControllerBase
    {
        [HttpGet]
        [Route("/[controller]")]

        public IActionResult Get(
            [FromServices] AppDbContext context)
        {
            return Ok(context.Atendimentos!
                        .Include(p => p.ListaProdutos)
                        .Include(k => k.GarconResponsavel)
                        .Include(l => l.MesaAtendida)
                        .ToListAsync()
                    );
        }

        [HttpGet("/[controller]/{id:int}")]

        public IActionResult GetById([FromRoute] int id,
                                    [FromServices] AppDbContext context)
        {
            var atendimentoModel = context.Atendimentos!.FirstOrDefault(e => e.AtendimentoID == id);
            if(atendimentoModel == null) {
                return NotFound();
            }
            return Ok(atendimentoModel);
        }

        [HttpPost("/[controller]")]

        public IActionResult Post([FromBody] AtendimentoModel ateModel,
                                [FromServices] AppDbContext context)
        {
            List<int> prodsId = new List<int>();

            foreach(var prod in ateModel.ListaProdutos!)
            {
                prodsId.Add(prod.ProdutoID!.Value);
            }

            ateModel.ListaProdutos = null;
            
            context.Atendimentos!.Add(ateModel);
            context.SaveChanges();

            
            return Created($"/{ateModel.AtendimentoID}", ateModel);
        }

        [HttpPut("/[controller]")]

        public IActionResult Put([FromRoute] int id,
                            [FromBody] AtendimentoModel ateModel,
                            [FromServices] AppDbContext context)
        {
            var AteModel = context.Atendimentos!.FirstOrDefault(e => e.AtendimentoID == id);
            if(AteModel == null) {
                return NotFound();
            }
            
            AteModel.AtendimentoID = ateModel.AtendimentoID;
            AteModel.MesaAtendida = ateModel.MesaAtendida;
            AteModel.GarconResponsavel = ateModel.GarconResponsavel;
            AteModel.ListaProdutos = ateModel.ListaProdutos;
            AteModel.HorarioAtendimento = ateModel.HorarioAtendimento;
            AteModel.PrecoTotal = ateModel.PrecoTotal;
            
            context.Atendimentos!.Update(AteModel);
            context.SaveChanges();
            return Ok(AteModel);
        }

        [HttpDelete("/[controller]")]

        public IActionResult Delete([FromRoute] int id,
                            [FromServices] AppDbContext context)
        {
            var AteModel = context.Atendimentos!.FirstOrDefault(e => e.AtendimentoID == id);
            if(AteModel == null) {
                return NotFound();
            }

            context.Atendimentos!.Remove(AteModel);
            context.SaveChanges();
            return Ok(AteModel);
        }

    }
}