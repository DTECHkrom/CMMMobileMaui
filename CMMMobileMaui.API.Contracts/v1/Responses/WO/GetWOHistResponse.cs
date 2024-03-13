using System;

namespace CMMMobileMaui.API.Contracts.v1.Responses.WO
{
    public class GetWOHistResponse
    {
        public string Person { get; set; }
        public DateTime Change_Time { get; set; }
        public string Source { get; set; }
        public string Value { get; set; }
    }
}
