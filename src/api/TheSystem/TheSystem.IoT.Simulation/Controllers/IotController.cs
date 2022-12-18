using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using TheSystem.IoT.Simulation.Services;

namespace TheSystem.IoT.Simulation.Controllers;

[Route("api/[controller]")]
public sealed class IotController : Controller
{
    private readonly TokenStorage _storage;
    private readonly HttpClient _client;

    public IotController(TokenStorage storage)
    {
        _storage = storage;
        _client = new HttpClient();
    }

    [HttpPost("token")]
    public IActionResult StoreToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            BadRequest("Token is empty");
        }
        
        _storage.Token= token;

        return Ok();
    }

    [HttpPost("not-to-smoke")]
    public async Task<IActionResult> CompleteDontSmokeAllDay()
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _storage.Token);
        var response = await _client.PostAsync("https://localhost:7184/api/achievement/Not to smoke", null);

        return response.IsSuccessStatusCode
            ? Ok("Don't smoke all day is finished")
            : BadRequest(await response.Content.ReadAsStringAsync());
    }
}
