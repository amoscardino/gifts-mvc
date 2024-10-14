using GiftsMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace GiftsMVC.Controllers;

public class GiftController(IHttpClientFactory httpClientFactory) : Controller
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("GiftsAPI");

    [HttpGet]
    public async Task<IActionResult> IndexAsync(long personId, long id = 0)
    {
        var personResponse = await _httpClient.GetAsync($"/api/person/{personId}");

        if (!personResponse.IsSuccessStatusCode)
            throw new Exception("Cannot retrieve data from the API");

        var person = await personResponse.Content.ReadFromJsonAsync<PersonDto>();

        if (id == 0)
            return View(new GiftDto { Person = person! });

        var response = await _httpClient.GetAsync($"/api/gift/{id}");

        if (!response.IsSuccessStatusCode)
            throw new Exception("Cannot retrieve data from the API");

        var gift = await response.Content.ReadFromJsonAsync<GiftDto>();

        return View(gift);
    }

    [HttpPost]
    public async Task<IActionResult> SaveAsync(GiftDto gift)
    {
        if (!ModelState.IsValid)
            return View("Index", gift);

        var response = gift.Id == 0
            ? await _httpClient.PostAsJsonAsync("/api/gift", gift)
            : await _httpClient.PutAsJsonAsync($"/api/gift/{gift.Id}", gift);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Cannot save data to the API");

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var response = await _httpClient.DeleteAsync($"/api/gift/{id}");

        if (!response.IsSuccessStatusCode)
            throw new Exception("Cannot delete data from the API");

        return RedirectToAction("Index", "Home");
    }
}
