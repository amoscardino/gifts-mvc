using GiftsMVC.Models;

namespace GiftsMVC.Services;

public class PersonService(IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("GiftsAPI");

    public async Task<IEnumerable<PersonDto>> GetPeopleAsync()
    {
        return (await _httpClient.GetFromJsonAsync<IEnumerable<PersonDto>>("api/person"))!;
    }

    public async Task<PersonDto> GetPersonAsync(long id)
    {
        return (await _httpClient.GetFromJsonAsync<PersonDto>($"api/person/{id}"))!;
    }

    public async Task CreatePersonAsync(PersonDto person)
    {
        await _httpClient.PostAsJsonAsync("api/person", person);
    }

    public async Task UpdatePersonAsync(PersonDto person)
    {
        await _httpClient.PutAsJsonAsync($"api/person/{person.Id}", person);
    }

    public async Task DeletePersonAsync(long id)
    {
        await _httpClient.DeleteAsync($"api/person/{id}");
    }
}