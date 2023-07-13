namespace Application.User.Commands.Create
{
    public class CreateUserCommandResponse 
    {
        public CreateUserCommandResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
