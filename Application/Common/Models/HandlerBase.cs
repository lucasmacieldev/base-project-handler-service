using Application.Common.Models.Response;

namespace Application.Common.Models
{
    public class HandlerBase<TResponse>
    {
        public HandlerBase()
        {
            Response = new ResponseApiBase<TResponse>();
        }
        public ResponseApiBase<TResponse> Response { get; private set; }
    }
}
