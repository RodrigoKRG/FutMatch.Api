using FutMatch.Domain.Enums;

namespace FutMatch.Domain.Entities
{
    public class User : AuditEntity
    {
        public string Login { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public byte[] Salt { get; set; } = new byte[16];
        public Roles Role { get; private set; } = Roles.User;
        public bool Active { get; private set; }

        public static User Build(string login, string password, byte[] salt, string userName)
        {
            var user = new User
            {
                Login = login,
                Password = password,
                Salt = salt,
                Active = true
            };
            user.SetAudit(userName);
            return user;
        }
    }
}
