namespace CMMMobileMaui.API.Contracts.v1.Responses.WO
{
    public class WODictResponse: DictBase
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
