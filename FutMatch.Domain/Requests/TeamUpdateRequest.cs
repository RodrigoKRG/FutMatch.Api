using FutMatch.Domain.Requests.Validators;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FutMatch.Domain.Requests;

public class TeamUpdateRequest : RequestBase
{
    
    [Required]
    public string Name { get; set; } = null!;

    public string? Shield { get; set; }

    public List<Guid> PlayerIds { get; set; } = new List<Guid>();


    [JsonIgnore]
    public string User { get; private set; } = null!;

    public void SetUser(string user)
    {
        User = user;
    }

    public override bool IsValid()
    {
        ValidationResult = new TeamUpdateRequestValidator()
            .Validate(this);
        return ValidationResult.IsValid;
    }
}
