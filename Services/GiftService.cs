using GiftsMVC.Models;

namespace GiftsMVC.Services;

public class GiftService(IHttpClientFactory httpClientFactory)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("GiftsAPI");

    public async Task<IEnumerable<GiftDto>> GetGiftsAsync()
    {
        return (await _httpClient.GetFromJsonAsync<IEnumerable<GiftDto>>("api/gift"))!;
    }

    public async Task<GiftDto> GetGiftAsync(long id)
    {
        return (await _httpClient.GetFromJsonAsync<GiftDto>($"api/gift/{id}"))!;
    }

    public async Task CreateGiftAsync(GiftDto gift)
    {
        await _httpClient.PostAsJsonAsync("api/gift", gift);
    }

    public async Task UpdateGiftAsync(GiftDto gift)
    {
        await _httpClient.PutAsJsonAsync($"api/gift/{gift.Id}", gift);
    }

    public async Task DeleteGiftAsync(long id)
    {
        await _httpClient.DeleteAsync($"api/gift/{id}");
    }
}