using LyteVentures.Todo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LyteVentures.Todo.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("GeneralIDPClient");
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateNewAccountRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync<CreateNewAccountRequest>("/api/account/register", request);
            return Ok(new BaseResponse
            {
                IsSuccess = response.IsSuccessStatusCode,
                Message = await response.Content.ReadAsStringAsync()
            });
        }
    }
}
