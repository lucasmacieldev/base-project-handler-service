namespace Application.User.Queries.GetToken
{
    public class GetTokenQueryResponse 
    {
        public GetTokenQueryResponse(string token)
        {
            Token = token;
        }

        public string Token { get; set; }
    }
}
