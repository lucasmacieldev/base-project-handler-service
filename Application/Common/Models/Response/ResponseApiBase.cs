using Newtonsoft.Json;

namespace Application.Common.Models.Response
{
    public class ResponseApiBase<T>
    {
        public bool Success { get { return Data != null; } }

        [JsonProperty("data")]
        public T Data { get; private set; }

        [JsonProperty("error")]
        public ResponseApiError Error { get; private set; }

        [JsonIgnore]
        public string DataResponseJson { get; private set; }

        [JsonIgnore]
        public string DataRequestJson { get; private set; }

        public void AddSuccess(T data) => Data = data;

        public void AddSuccess(T data, string dataRequestJson, string dataResponseJson)
        {
            Data = data;
            DataRequestJson = dataRequestJson;
            DataResponseJson = dataResponseJson;
        }

        public void AddError(int code, string message) => Error = new ResponseApiError { Code = code, Message = message };

        public void AddError(int code, string message, string dataRequestJson, string dataResponseJson)
        {
            DataRequestJson = dataRequestJson;
            DataResponseJson = dataResponseJson;
            Error = new ResponseApiError { Code = code, Message = message };
        }

        public void AddError(string message) => this.Error = new ResponseApiError { Code = -1, Message = message };
    }
}
