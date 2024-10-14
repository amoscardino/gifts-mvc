using GiftsMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace GiftsMVC.Controllers;

public class PersonController(IHttpClientFactory httpClientFactory) : Controller
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("GiftsAPI");

    [HttpGet]
    public async Task<IActionResult> IndexAsync(long id = 0)
    {
        if (id == 0)
            return View(new PersonDto());

        var response = await _httpClient.GetAsync($"/api/person/{id}");

        if (!response.IsSuccessStatusCode)
            throw new Exception("Cannot retrieve data from the API");

        var person = await response.Content.ReadFromJsonAsync<PersonDto>();

        return View(person);
    }

    [HttpPost]
    public async Task<IActionResult> SaveAsync(PersonDto person)
    {
        if (!ModelState.IsValid)
            return View("Index", person);

        var response = person.Id == 0
            ? await _httpClient.PostAsJsonAsync("/api/person", person)
            : await _httpClient.PutAsJsonAsync($"/api/person/{person.Id}", person);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Cannot save data to the API");

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        var response = await _httpClient.DeleteAsync($"/api/person/{id}");

        if (!response.IsSuccessStatusCode)
            throw new Exception("Cannot delete data from the API");

        return RedirectToAction("Index", "Home");
    }
}
