using FutMatch.Domain.Requests;

namespace FutMatch.Domain.Entities
{
    public class Player : UserProfile
    {
        public string Name { get; set; } = null!;
        public string Cpf { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public virtual List<FieldPosition>? Positions { get; set; }
        public virtual List<Team>? Teams { get; set; }

        public static Player Build(PlayerCreateRequest request)
        {
            var player = new Player
            {
                Name = request.Name,
                Cpf = request.Cpf,
                DateOfBirth = request.BirthDate,
                Email = request.Email,
                Phone = request.PhoneNumber,
            };
            player.SetAudit(request.User);
            return player;
        }

        public void Update(PlayerUpdateRequest request)
        {
            Name = request.Name ?? Name;
            Cpf = request.Cpf ?? Cpf;
            DateOfBirth = request.BirthDate ?? DateOfBirth;
            Email = request.Email ?? Email;
            Phone = request.PhoneNumber ?? Phone;
            SetAudit(request.User);
        }

        public void AddPosition(FieldPosition position)
        {
            if (Positions == null)
            {
                Positions = new List<FieldPosition>();
            }
            Positions.Add(position);
        }

        public void SetUser(User user)
        {
            User = user;
        }
    }
}
