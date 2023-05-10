using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GerenRest.RazorPages.Pages.Atendimento
{
    public class Details : PageModel
    {
        public AtendimentoModel AtenModel { get; set; } = new();
        public Details()
        {

        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id == null) {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                string url = $"http://localhost:5239/Atendimento/{id}";

                var response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return NotFound();
                }
                
                var content = await response.Content.ReadAsStringAsync();
                var atendimentoModelRoot = JsonConvert.DeserializeObject<AtendimentoModelRoot>(content)!;
                AtenModel = atendimentoModelRoot.Atendimento!;
            }

            if(AtenModel == null) {
                return NotFound();
            }

            return Page();
        }
    }
}