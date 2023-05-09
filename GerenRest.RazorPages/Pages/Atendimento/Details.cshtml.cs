using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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

            AtendimentoModelRoot atenModel = new AtendimentoModelRoot();

            using (var httpClient = new HttpClient())
            {
                string url = $"http://localhost:5239/Atendimento/{id}";

                var requestMes = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await httpClient.SendAsync(requestMes);
                
                var content = await response.Content.ReadAsStringAsync();
                
                atenModel = JsonConvert.DeserializeObject<AtendimentoModelRoot>(content)!;   
            }

            if(atenModel == null) {
                return NotFound();
            }

            return Page();
        }
    }
}