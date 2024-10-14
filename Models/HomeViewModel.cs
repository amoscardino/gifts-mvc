namespace GiftsMVC.Models;

public class HomeViewModel
{
    public IEnumerable<GiftDto> Gifts { get; set; } = [];
    public IEnumerable<PersonDto> People { get; set; } = [];
}