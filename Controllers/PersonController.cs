using GiftsMVC.Models;
using GiftsMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace GiftsMVC.Controllers;

public class PersonController(PersonService personService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> IndexAsync(long id = 0)
    {
        if (id == 0)
            return View(new PersonDto());

        var person = await personService.GetPersonAsync(id);

        return View(person);
    }

    [HttpPost]
    public async Task<IActionResult> SaveAsync(PersonDto person)
    {
        if (!ModelState.IsValid)
            return View("Index", person);

        if (person.Id == 0)
            await personService.CreatePersonAsync(person);
        else
            await personService.UpdatePersonAsync(person);

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAsync(long id)
    {
        await personService.DeletePersonAsync(id);

        return RedirectToAction("Index", "Home");
    }
}
