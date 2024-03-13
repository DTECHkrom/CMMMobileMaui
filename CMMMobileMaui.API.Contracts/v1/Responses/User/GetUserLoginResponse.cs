
using Newtonsoft.Json;

namespace CMMMobileMaui.API.Contracts.v1.Responses.User
{
    public class GetUserLoginResponse
    {
        public short PersonID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string LangCode
        {
            get;
            set;
        }

        [JsonProperty("RigthMatrix")]
        public GetUserRightResponse GetUserRightResponse
        {
            get;
            set;
        }
    }
}
