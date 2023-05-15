using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models {
    public class ProdutoModel {
        public int? ProdutoID { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public float? Preco { get; set; }
        public int? CategoriaID { get; set; }
        
        [ForeignKey("CategoriaID")]
        public CategoriaModel? Categoria { get; set; }
    }
}