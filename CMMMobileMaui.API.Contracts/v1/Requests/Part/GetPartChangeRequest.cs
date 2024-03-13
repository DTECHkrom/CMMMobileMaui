namespace CMMMobileMaui.API.Contracts.v1.Requests.Part
{
    public class GetPartChangeRequest
    {
        public int PartID
        {
            get;
            set;
        }

        public string Lang
        {
            get;
            set;
        }

        public short? PersonID
        {
            get;
            set;
        }

        public bool OnlyTaken
        {
            get;
            set;
        }
    }
}
