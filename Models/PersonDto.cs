using System.ComponentModel.DataAnnotations;

namespace GiftsMVC.Models;

public class PersonDto
{
    public long Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}
