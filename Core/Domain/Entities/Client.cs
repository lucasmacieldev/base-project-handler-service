using Domain.Enumerators;

namespace Domain.Entities
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public DateTime DateBirth { get; set; }
        public ClientType Type { get; set; }
    }
}
