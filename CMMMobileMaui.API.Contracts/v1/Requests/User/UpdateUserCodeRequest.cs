namespace CMMMobileMaui.API.Contracts.v1.Requests.User
{
    public class UpdateUserCodeRequest
    {
        public short PersonID
        {
            get;
            set;
        }

        public string Code
        {
            get;
            set;
        }
    }
}
