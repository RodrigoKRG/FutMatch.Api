namespace FutMatch.Domain.Entities
{
    public class BaseEntity
    {
        public long Id { get; private set; }

        public void SetId(long id)
        {
            Id = id;
        }
    }
}
