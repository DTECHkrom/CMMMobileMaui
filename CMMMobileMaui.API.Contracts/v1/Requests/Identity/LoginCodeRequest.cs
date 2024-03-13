namespace CMMMobileMaui.API.Contracts.v1.Requests.Identity
{
    public class LoginCodeRequest
    {
        public string Code
        {
            get;
            set;
        }

        public string Mobile_Info { get; set; }
    }
}
