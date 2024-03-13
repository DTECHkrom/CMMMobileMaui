namespace CMMMobileMaui.API.Contracts.v1.Requests.Part
{
    public class UpdatePartImageRequest
    {
        public int PartID
        {
            get;
            set;
        }

        public short PersonID
        {
            get;
            set;
        }

        public byte[]? Image
        {
            get;
            set;
        }
    }
}
