using Domain.Enumerators;

namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public string Password { get; set; }
        public string Key { get; set; }
        public bool Active { get; set; }
        public DateTime BirthDate { get; set; }
        public UserType Type { get; set; }
        public string DocumentProfessional { get; set; }
    }
}
