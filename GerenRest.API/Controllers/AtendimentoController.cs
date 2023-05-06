using GerenRest.API.Data;
using GerenRest.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace GerenRest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtendimentoController : ControllerBase
    {
        [HttpGet]
        [Route("/")]

        public IActionResult Get(
            [FromServices] AppDbContext context)
        {
            return Ok(context.Atendimentos!.ToList());
        }

        [HttpGet("/{id:int}")]

        public IActionResult GetById([FromRoute] int id,
                                    [FromServices] AppDbContext context)
        {
            var atendimentoModel = context.Atendimentos!.FirstOrDefault(e => e.AtendimentoID == id);
            if(atendimentoModel == null) {
                return NotFound();
            }
            return Ok(atendimentoModel);
        }

        [HttpPost("/")]

        public IActionResult Post([FromBody] AtendimentoModel ateModel,
                             [FromServices] AppDbContext context)
        {
            context.Atendimentos!.Add(ateModel);
            context.SaveChanges();
            return Created($"/{ateModel.AtendimentoID}", ateModel);
        }

        [HttpPut("/")]

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

        [HttpDelete("/")]

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