namespace CMMMobileMaui.API.Contracts.v1.Responses.Device
{
    public class GetMainLocationResponse
    {
        public int LocationID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public int? CategoryID
        {
            get;
            set;
        }

        public bool? IsWriteText
        {
            get;
            set;
        }

        public string TabSource
        {
            get;
            set;
        }

        public int? MainLocationID
        {
            get;
            set;
        }

        public int? BranchID
        {
            get;
            set;
        }

        public int? SubLocationID
        {
            get;
            set;
        }

        public bool IsMulti
        {
            get;
            set;
        }
    }
}
