namespace CMMMobileMaui.API.Contracts.v1.Requests.User
{
    public class GetUserLoginByPasswordRequest
    {
        public short PersonID
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }
    }
}
