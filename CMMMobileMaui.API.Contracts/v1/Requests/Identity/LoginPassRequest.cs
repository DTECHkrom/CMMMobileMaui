namespace CMMMobileMaui.API.Contracts.v1.Requests.Identity
{
    public class LoginPassRequest
    {
        public short PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mobile_Info { get; set; }
    }
}
