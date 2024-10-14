using GiftsMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace GiftsMVC.Controllers;

public class HomeController(IHttpClientFactory httpClientFactory) : Controller
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("GiftsAPI");

    public async Task<IActionResult> IndexAsync()
    {
        var giftsTask = _httpClient.GetAsync("/api/gift");
        var peopleTask = _httpClient.GetAsync("/api/person");

        await Task.WhenAll(giftsTask, peopleTask);

        var giftsResponse = giftsTask.Result;
        var peopleResponse = peopleTask.Result;

        if (!giftsResponse.IsSuccessStatusCode || !peopleResponse.IsSuccessStatusCode)
            throw new Exception("Cannot retrieve data from the API");

        var gifts = await giftsResponse.Content.ReadFromJsonAsync<IEnumerable<GiftDto>>();
        var people = await peopleResponse.Content.ReadFromJsonAsync<IEnumerable<PersonDto>>();

        return View(new HomeViewModel
        {
            Gifts = gifts ?? [],
            People = people ?? []
        });
    }
}
