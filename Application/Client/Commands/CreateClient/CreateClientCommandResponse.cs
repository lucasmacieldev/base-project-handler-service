namespace Application.Client.Commands.CreateClient
{
    public class CreateClientCommandResponse 
    {
        public CreateClientCommandResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
