using Domain.Enumerators;

namespace Domain.Entities
{
    public class Client : BaseEntity
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public ClientType Type { get; set; }
    }
}
