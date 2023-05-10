using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtendimentoController : ControllerBase
    {
        AtendimentoModel AtendimentoModel = new AtendimentoModel();

        [HttpGet]
        [Route("/[controller]")]

        public IActionResult Get(
            [FromServices] AppDbContext context)
        {
            return Ok(context.Atendimentos!
                        .Include(p => p.ListaProdutos)!
                            .ThenInclude(o => o.Categoria)
                        .Include(k => k.GarconResponsavel)
                        .Include(l => l.MesaAtendida)
                        .ToListAsync()
                    );
        }

        [HttpGet("/[controller]/{id:int}")]

        public IActionResult GetById([FromRoute] int id,
                                    [FromServices] AppDbContext context)
        { 
            var atenModel = context.Atendimentos!
                                .Include(p => p.ListaProdutos)!
                                    .ThenInclude(o => o.Categoria)
                                .Include(k => k.GarconResponsavel)
                                .Include(l => l.MesaAtendida)
                                .FirstOrDefaultAsync(e => e.AtendimentoID == id);
            return Ok(atenModel);
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
            
            var prodAte = new AtendimentoProdutoModel();

            foreach(int idProd in prodsId)
            {
                prodAte.AtendimentoID = ateModel.AtendimentoID;
                prodAte.ProdutoID = idProd;
                context.AtendimentoProduto!.Add(prodAte);
            }

            context.SaveChanges();
            return Created($"/{ateModel.AtendimentoID}", ateModel);
        }

        [HttpPut("/[controller]/{id:int}")]

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

        [HttpDelete("/[controller]/{id:int}")]

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