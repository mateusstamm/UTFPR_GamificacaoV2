using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GerenRest.RazorPages.Pages.Atendimento
{
    public class Create : PageModel
    {
        public class QuantProduto {
            public ProdutoModel? Produto { get; set; }
            public int Quantidade { get; set; }
        }

        [BindProperty]
        public AtendimentoModel AtenModel { get; set; } = new();
        public List<GarconModel>? GarconModel { get; set; }
        public List<MesaModel>? MesaModel { get; set; }
        public List<ProdutoModel>? ProdModel { get; set; }
        [BindProperty]
        public int? GarconId { get; set; }
        [BindProperty]
        public int? MesaId { get; set; }
        [BindProperty]
        public int? ProdId { get; set; }
        public List<AtendimentoModel>? ListAtend { get; set; }
        public Create()
        {
            
        }
    
        public async Task<IActionResult> OnPostAsync()
        {
            
            if(!ModelState.IsValid)
                return Page();

            int[] prodConsumidos = Request.Form["ProdSelec"].Select(int.Parse!).ToArray();
            
            if(prodConsumidos.Length == 0) {
                TempData["ErroSelecaoProd"] = "Nenhum produto foi selecionado!";
                return RedirectToPage("/Atendimento/Create");
            }

            using (var httpClient = new HttpClient())
            {
                var url = $"http://localhost:5239/api/garcon/{GarconId}";
                var response = await httpClient.GetAsync(url);
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    AtenModel.GarconResponsavel = JsonConvert.DeserializeObject<GarconModel>(content);
                }

            }
            
            AtenModel.HorarioAtendimento = DateTime.Now;

            using (var httpClient = new HttpClient())
            {
                var url = $"http://localhost:5239/api/mesa/{MesaId}";
                var response = await httpClient.GetAsync(url);
                if(response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    AtenModel.MesaAtendida = JsonConvert.DeserializeObject<MesaModel>(content);
                    AtenModel.MesaAtendida!.Ocupada = "Livre";
                    AtenModel.MesaAtendida!.HoraAbertura = DateTime.Now;
                }

            }

            try {
                _context.Add(AtenModel);
                await _context.SaveChangesAsync();
                ListAtend = await _context.Atendimentos!.Where(p => p.AtendimentoID == AtenModel.AtendimentoID).ToListAsync();

                return RedirectToPage("/Atendimento/Index");
                
            } catch(DbUpdateException) {
                return Page();
            }
        }

        public async Task<IActionResult> OnGetAsync() {
            // using (var httpClient = new HttpClient())
            // {
            //     var url = $"http://localhost:5239/api/garcon/";
            //     var response = await httpClient.GetAsync(url);
            //     if(response.IsSuccessStatusCode)
            //     {
            //         var content = await response.Content.ReadAsStringAsync();
            //          = JsonConvert.DeserializeObject<List<MesaModel>>(content);
                    
            //     }

            // }
            var httpClient = new HttpClient();
            var url = $"http://localhost:{HttpContext.Request.Host.Port.ToString()}/api/garcon/";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsStringAsync();

            GarconModel = JsonConvert.DeserializeObject<List<GarconModel>>(content!);

            if (GarconModel == null || GarconModel.Count == 0) {
                TempData["ErroGarcon"] = "Não há garçons disponíveis!";
                return RedirectToPage("/Atendimento/Index");
            }

            MesaModel = JsonConvert.DeserializeObject<List<MesaModel>>(content!);

            if (MesaModel == null || MesaModel.Count == 0) {
                TempData["ErroMesaRegistro"] = "Não há mesas registradas!";
                return RedirectToPage("/Atendimento/Index");
            }

            int countMesasOcupadas = 0;
            foreach(var mesaModel in MesaModel) {
                if(mesaModel.Ocupada == "Ocupada") {
                    countMesasOcupadas++;
                }
            }

            if (countMesasOcupadas == 0) {
                TempData["ErroMesasOcupadas"] = "Mesas livres, não há atendimento!";
                return RedirectToPage("/Atendimento/Index");
            }

            ProdModel = JsonConvert.DeserializeObject<List<ProdutoModel>>(content!);

            if (ProdModel == null || ProdModel.Count == 0) {
                TempData["ErroProduto"] = "Não há produtos registrados!";
                return RedirectToPage("/Atendimento/Index");
            }
            
            return Page();
        }
    }
}