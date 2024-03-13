using Newtonsoft.Json;
using CMMMobileMaui.API.Contracts.v1.Responses.User;

namespace CMMMobileMaui.API.Contracts.v1.Responses.Identity
{
    public class LoginResponse
    {
        public short PersonID { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string LangCode { get; set; }

        [JsonProperty("RigthMatrix")]
        public GetUserRightResponse GetUserRightResponse
        {
            get;
            set;
        }
    }
}
