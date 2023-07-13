using Domain.Entities;

namespace Application.User.Queries.Detail
{
    public class GetUserQueryResponse 
    {
        public GetUserQueryResponse(Domain.Entities.User user)
        {
            User = user;
        }

        public Domain.Entities.User User { get; set; }
    }
}
