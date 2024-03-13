namespace CMMMobileMaui.API.Contracts.v1.Responses.Act
{
    public class WOActDictResponse: DictBase
    {
        public int? MachineCategoryID
        {
            get;
            set;
        }

        public bool Is_Default
        {
            get;
            set;
        }
    }
}
