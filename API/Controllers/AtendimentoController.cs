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
        [HttpGet]
        [Route("/[controller]")]

        public async Task<IActionResult> Get(
            [FromServices] AppDbContext context)
        {
            var atModel = context.Atendimentos!
                                .Include(p => p.ListaProdutos)!
                                    .ThenInclude(o => o.Categoria)
                                .Include(k => k.GarconResponsavel)
                                .Include(l => l.MesaAtendida)
                                .ToListAsync()
                                .Result;

            foreach(var atendimento in atModel)
            {
                var quants = await context.AtendimentoProduto!
                                    .Where(k => k.AtendimentoID == atendimento.AtendimentoID)
                                    .Select(o => o.Quantidade!.Value)
                                    .ToListAsync();
                atendimento.ListaQuantidade = quants;
            }

            return Ok(atModel);
        }

        [HttpGet("/[controller]/{id:int}")]

        public IActionResult GetById([FromRoute] int id,
                                    [FromServices] AppDbContext context)
        { 
            return Ok(context.Atendimentos!
                                .Include(p => p.ListaProdutos)!
                                    .ThenInclude(o => o.Categoria)
                                .Include(k => k.GarconResponsavel)
                                .Include(l => l.MesaAtendida)
                                .FirstOrDefaultAsync(e => e.AtendimentoID == id)
                                .Result);
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
            
            AtendimentoProdutoModel prodAte = new AtendimentoProdutoModel();
            int cont = 0;

            foreach(int idProd in prodsId)
            {
                context.AtendimentoProduto!.Add(new AtendimentoProdutoModel() {
                    AtendimentoID = ateModel.AtendimentoID,
                    ProdutoID = idProd,
                    Quantidade = ateModel.ListaQuantidade![cont++]
                });
            }

            context.SaveChanges();
            return Created($"/{ateModel.AtendimentoID}", ateModel);
        }

        [HttpPut("/[controller]/{id:int}")]

        public IActionResult Put([FromRoute] int id,
                            [FromBody] AtendimentoModel ateModel,
                            [FromServices] AppDbContext context)
        {
            var AteModel = context.Atendimentos!.FirstOrDefaultAsync(e => e.AtendimentoID == id).Result;

            if(AteModel == null) {
                return NotFound();
            }
            
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
            var AteModel = context.Atendimentos!.FirstOrDefaultAsync(e => e.AtendimentoID == id).Result!;

            if(AteModel == null) {
                return NotFound();
            }

            context.Atendimentos!.Remove(AteModel);
            context.SaveChanges();
            return Ok(AteModel);
        }
    }
}