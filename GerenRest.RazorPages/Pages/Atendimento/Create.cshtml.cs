using System.Net.Http.Headers;
using System.Text;
using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GerenRest.RazorPages.Pages.Atendimento
{
    public class Create : PageModel
    {   
        private class DadosJson {
            public int? AtendimentoID { get; set; } = null;
            public int? MesaID { get; set; }
            public int? GarconID { get; set; }
            public List<ProdutoModel>? ListaProdutos { get; set; } = new();
            public List<int>? ListaQuantidade { get; set; } = new();
            public DateTime HorarioAtendimento { get; set; }
            public float PrecoTotal { get; set; }
        }
        [BindProperty]
        public AtendimentoModel AtenModel { get; set; } = new();
        public List<GarconModel>? GarconModel { get; set; }
        public List<MesaModel>? MesaModel { get; set; }
        public List<ProdutoModel>? ProdModel { get; set; }
        [BindProperty]
        public int GarconId { get; set; }
        [BindProperty]
        public int MesaId { get; set; }
        [BindProperty]
        public int ProdId { get; set; }
        public List<AtendimentoModel>? ListAtend { get; set; }
        public Create()
        {
            
        }
        public async Task<IActionResult> OnPostAsync()
        {
            
            if(!ModelState.IsValid)
                return Page();

            DadosJson dadosJson = new DadosJson();
            dadosJson.GarconID = GarconId;
            dadosJson.MesaID = MesaId;

            using (var httpClient = new HttpClient())
            {
                string url = $"http://localhost:5239/Mesa/{MesaId}";
                var response = await httpClient.GetAsync(url);
                
                var content = await response.Content.ReadAsStringAsync();
                var mesaAtendida = JsonConvert.DeserializeObject<MesaModel>(content)!;

                mesaAtendida.Ocupada = "Ocupada";
                mesaAtendida.HoraAbertura = DateTime.Now;

                string jsonData = JsonConvert.SerializeObject(mesaAtendida);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var arqJson = new StringContent(jsonData, Encoding.UTF8, "application/json");
                await httpClient.PutAsync(url, arqJson);
            }

            int[] prodConsumidos = Request.Form["ProdSelec"].Select(int.Parse!).ToArray();
            int[] prodQuantidade = Request.Form["QuantSelect"].Select(int.Parse!).ToArray();
            
            if(prodConsumidos.Length == 0) {
                TempData["ErroSelecaoProd"] = "Nenhum produto foi selecionado!";
                return RedirectToPage("/Atendimento/Create");
            }

            AtenModel.PrecoTotal = 0;

            foreach(var quantProd in prodQuantidade)
            {
                dadosJson.ListaQuantidade!.Add(quantProd);
            }

            int cont = 0;

            using (var httpClient = new HttpClient())
            {
                foreach(var idProd in prodConsumidos)
                {
                    string url = $"http://localhost:5239/Produto/{idProd}";
                    var requestMes = new HttpRequestMessage(HttpMethod.Get, url);
                    var response = await httpClient.SendAsync(requestMes);
                
                    var content = await response.Content.ReadAsStringAsync();
                    var prod = JsonConvert.DeserializeObject<ProdutoModel>(content)!;
                    
                    AtenModel.PrecoTotal += prod.Preco * dadosJson.ListaQuantidade![cont++];
                    dadosJson.ListaProdutos!.Add(new ProdutoModel() {
                        ProdutoID = idProd
                    });
                }
            }

            dadosJson.PrecoTotal = AtenModel.PrecoTotal.Value;
            dadosJson.HorarioAtendimento = DateTime.Now;

            string json = JsonConvert.SerializeObject(dadosJson);

            using (HttpClient client = new HttpClient())
            {
                string apiUrl = "http://localhost:5239/Atendimento";

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(json, Encoding.UTF8, "application/json");
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
                if(mesaModel.Ocupada == "Livre") {
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