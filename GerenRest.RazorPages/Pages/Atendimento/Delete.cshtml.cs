using GerenRest.RazorPages.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GerenRest.RazorPages.Pages.Atendimento
{
    public class Delete : PageModel
    {

        [BindProperty]
        public AtendimentoModel AtenModel { get; set; } = new();
        public Delete()
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
                AtenModel = JsonConvert.DeserializeObject<AtendimentoModel>(content)!;
            }

            if(AtenModel == null) {
                return NotFound();
            }

            return Page();
        }
    
        public async Task<IActionResult> OnPostAsync(int id)
        {

            using (var httpClient = new HttpClient())
            {
                string url = $"http://localhost:5239/Atendimento/{id}";
                var response = await httpClient.DeleteAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return NotFound();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}