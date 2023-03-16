using System.Text.Json;

namespace Bookify.Models
{
    public record Error
    {
        public int StatusCode { get; init; }

        public string Message { get; init; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
