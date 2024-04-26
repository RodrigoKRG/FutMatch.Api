using FutMatch.Domain.Entities;

namespace FutMatch.Domain.Responses
{
    public class PlayerResponse
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime BirthTime { get; set; }

        public static PlayerResponse Build(Player user)
        {
            return new PlayerResponse
            {
                Id = user.Id,
                Name = user.Name,
                Cpf = user.Cpf,
                Email = user.Email,
                PhoneNumber = user.Phone,
                BirthTime = user.DateOfBirth,
            };
        }
    }
}
