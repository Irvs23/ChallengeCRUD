using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CatFactController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRandomCatFact()
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync("https://catfact.ninja/fact");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return Ok(json);
            }
            else
            {
                return StatusCode((int)response.StatusCode);
            }
        }
    }
}
