using System.ComponentModel.DataAnnotations.Schema;

namespace GerenRest.API.Models {
    public class AtendimentoModel {
        public int? AtendimentoID { get; set; }
        public int? MesaID { get; set; }
        public int? GarconID { get; set; }
        [ForeignKey("MesaID")]
        public MesaModel? MesaAtendida { get; set; }
        [ForeignKey("GarconID")]
        public GarconModel? GarconResponsavel { get; set; }
        public List<ProdutoModel>? ListaProdutos { get; set; }
        public DateTime? HorarioAtendimento { get; set; }
        public float? PrecoTotal { get; set; }
    }
}