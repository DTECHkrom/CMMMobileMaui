using System;
using System.Threading.Tasks;

namespace CMMMobileMaui.SCAN
{
    public interface IScanType
    {
        Action<object> UIMethod
        {
            get;
            set;
        }

        bool IsValid(string code);

        IScanType GetSubScan();

        string GetParsedCode();

        Task ScanMethod();
    }
}
