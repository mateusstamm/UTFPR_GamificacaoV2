using System.Net.Http.Headers;
using System.Text;
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

            var data = new {
                int AtendimentoID = null,
                
            }
            const char aspa = '"';
            var jsonAten = "{" + aspa + "AtendimentoID" + aspa + ":null, " + aspa + "MesaAtendida" + aspa + ": { " + aspa + "MesaID" + aspa + ": "
                + MesaId + " }, " + aspa + "GarconResponsavel" + aspa + ": { " + aspa + "GarconID" + aspa + ": " + GarconId + "}, " + aspa + "ListaProdutos" + aspa + ": [ ";

            int[] prodConsumidos = Request.Form["ProdSelec"].Select(int.Parse!).ToArray();
            
            if(prodConsumidos.Length == 0) {
                TempData["ErroSelecaoProd"] = "Nenhum produto foi selecionado!";
                return RedirectToPage("/Atendimento/Create");
            }

            List<ProdutoModel> listProd = new List<ProdutoModel>();
            AtenModel.PrecoTotal = 0;
            int count = 1;

            using (var httpClient = new HttpClient())
            {
                foreach(var idProd in prodConsumidos)
                {
                    if(count != prodConsumidos.Count())
                        jsonAten += "{ "+ aspa +"ProdutoID"+ aspa +": " + idProd + "},";
                    else
                        jsonAten += "{ "+ aspa +"ProdutoID"+ aspa +": " + idProd + "} ],";

                    string url = $"http://localhost:5239/Produto/{idProd}";
                    var requestMes = new HttpRequestMessage(HttpMethod.Get, url);
                    var response = await httpClient.SendAsync(requestMes);
                
                    var content = await response.Content.ReadAsStringAsync();
                    var prod = JsonConvert.DeserializeObject<ProdutoModel>(content)!;
                    AtenModel.PrecoTotal += prod.Preco;
                    count++;
                }
            }

            jsonAten += " "+ aspa +"HorarioAtendimento"+ aspa +": " + DateTime.Now + ", "+ aspa +"PrecoTotal"+ aspa +": " + AtenModel.PrecoTotal + " }";

            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "http://localhost:5239/Atendimento";

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(jsonAten, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Atendimento/Index");
                }
                else
                {
                    return StatusCode((int)response.StatusCode);
                }
            }
            
        }

        public async Task<IActionResult> OnGetAsync() {
            using (var httpClient = new HttpClient())
            {
                string url = "http://localhost:5239/Garcon";

                var requestMes = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await httpClient.SendAsync(requestMes);
                
                var content = await response.Content.ReadAsStringAsync();
                GarconModel = JsonConvert.DeserializeObject<List<GarconModel>>(content!);
            }

            if (GarconModel == null || GarconModel.Count == 0) {
                TempData["ErroGarcon"] = "Não há garçons disponíveis!";
                return RedirectToPage("/Atendimento/Index");
            }

            using (var httpClient = new HttpClient())
            {
                string url = "http://localhost:5239/Mesa";

                var requestMes = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await httpClient.SendAsync(requestMes);
                
                var content = await response.Content.ReadAsStringAsync();
                MesaModel = JsonConvert.DeserializeObject<List<MesaModel>>(content!);
            }

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

            using (var httpClient = new HttpClient())
            {
                string url = "http://localhost:5239/Produto";

                var requestMes = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await httpClient.SendAsync(requestMes);
                
                var content = await response.Content.ReadAsStringAsync();
                ProdModel = JsonConvert.DeserializeObject<List<ProdutoModel>>(content!);
            }
            
            if (ProdModel == null || ProdModel.Count == 0) {
                TempData["ErroProduto"] = "Não há produtos registrados!";
                return RedirectToPage("/Atendimento/Index");
            }
            
            return Page();
        }
    }
}