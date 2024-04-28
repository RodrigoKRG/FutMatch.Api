using FutMatch.Domain.Requests.Validators;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FutMatch.Domain.Requests;

public class TeamCreateRequest : RequestBase
{
    public long? Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public string? Shield { get; set; }

    public List<long> PlayerIds { get; set; } = new List<long>();

    [JsonIgnore]
    public long UserId { get; set; }

    public void SetUserId(long userId)
    {
        UserId = userId;
    }


    public override bool IsValid()
    {
        ValidationResult = new TeamCreateRequestValidator()
            .Validate(this);
        return ValidationResult.IsValid;
    }
}
