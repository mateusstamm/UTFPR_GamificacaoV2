using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class AtendimentoProdutoModel
    {
        public int? ProdutoID { get; set; }
        public int? AtendimentoID { get; set; }
        public int? Quantidade { get; set; }
        [ForeignKey("AtendimentoID")]
        public AtendimentoModel? Atendimento { get; set; }
        [ForeignKey("ProdutoID")]
        public ProdutoModel? Produto { get; set; }
    }
}