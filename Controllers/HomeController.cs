using GiftsMVC.Models;
using GiftsMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace GiftsMVC.Controllers;

public class HomeController(PersonService personService, GiftService giftService) : Controller
{
    public async Task<IActionResult> IndexAsync()
    {
        var giftsTask = giftService.GetGiftsAsync();
        var peopleTask = personService.GetPeopleAsync();

        await Task.WhenAll(giftsTask, peopleTask);

        return View(new HomeViewModel
        {
            Gifts = giftsTask.Result,
            People = peopleTask.Result
        });
    }
}
