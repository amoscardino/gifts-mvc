using GiftsMVC.Models;
using GiftsMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace GiftsMVC.Controllers;

public class GiftController(GiftService giftService, PersonService personService) : Controller
{

    [HttpGet]
    public async Task<IActionResult> IndexAsync(long personId, long id = 0)
    {
        if (id == 0)
        {
            var person = await personService.GetPersonAsync(personId);
            return View(new GiftDto { Person = person! });
        }

        var gift = await giftService.GetGiftAsync(id);

        return View(gift);
    }

    [HttpPost]
    public async Task<IActionResult> SaveAsync(GiftDto gift)
    {
        if (!ModelState.IsValid)
            return View("Index", gift);

        if (gift.Id == 0)
            await giftService.CreateGiftAsync(gift);
        else
            await giftService.UpdateGiftAsync(gift);

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await giftService.DeleteGiftAsync(id);

        return RedirectToAction("Index", "Home");
    }
}
