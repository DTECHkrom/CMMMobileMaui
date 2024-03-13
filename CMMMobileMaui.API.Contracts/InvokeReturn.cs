using Newtonsoft.Json;

namespace CMMMobileMaui.API.Contracts
{
    public class InvokeReturn<T>
    {
        [JsonProperty("Data")]
        public T? Data { get; set; }

        [JsonProperty("IsValid")]
        public bool IsValid { get; set; }

        [JsonProperty("Errors")]
        public List<string> Errors { get; set; }

        public InvokeReturn()
        {
            Data = default;
            IsValid = false;
            Errors = new List<string>();
        }

        public string GetErrorMsg() =>
            Errors.Count == 1 ? Errors[0]
            : Errors.Count > 1 ? string.Join("|", Errors)
            : string.Empty;

        public bool IsErrorMsg() =>
            Errors != null
            && Errors.Count > 0;
    }
}
