using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace FutMatch.Domain.Requests
{
    public abstract class RequestBase
    {
        [JsonIgnore]
        public DateTime Timestamp { get; private set; }

        [JsonIgnore]
        public ValidationResult? ValidationResult { get; protected set; }

        [JsonIgnore]
        public string? user { get; private set; }

        public void SetUser(string user)
        {
            this.user = user;
        }

        protected RequestBase()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }
}
