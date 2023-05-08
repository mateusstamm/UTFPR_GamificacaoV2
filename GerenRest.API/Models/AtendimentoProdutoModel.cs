using System.ComponentModel.DataAnnotations.Schema;

namespace GerenRest.API.Models
{
    public class AtendimentoProdutoModel
    {
        public int? ProdutoID { get; set; }
        public int? AtendimentoID { get; set; }
        [ForeignKey("AtendimentoID")]
        public AtendimentoModel? Atendimento { get; set; }
        [ForeignKey("ProdutoID")]
        public ProdutoModel? Produto { get; set; }
    }
}