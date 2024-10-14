using System.Text.Json.Serialization;

namespace GiftsMVC.Models;

[JsonConverter(typeof(JsonStringEnumConverter<GiftStatus>))]
public enum GiftStatus
{
    Idea,
    Purchased,
    Arrived,
    Wrapped,
}
