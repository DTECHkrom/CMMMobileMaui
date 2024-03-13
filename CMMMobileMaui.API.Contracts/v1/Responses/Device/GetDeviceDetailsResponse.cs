using Newtonsoft.Json;

namespace CMMMobileMaui.API.Contracts.v1.Responses.Device
{
    public class GetDeviceDetailsResponse
    {
        public int Display_Index
        {
            get;
            set;
        }

        public string Property_Name
        {
            get;
            set;
        }

        public string Property_Value
        {
            get;
            set;
        }

        public string Property_Type
        {
            get;
            set;
        }

        [JsonIgnore]
        public string DisplayName
        {
            get;
            private set;
        }

        public void SetDisplayName()
        {
            DisplayName = Models.COMMON.Container.TextDictionaryResource.GetText("dev_" + Property_Name);
        }
    }
}
