using CMMMobileMaui.API.Contracts.v1.Responses.Part;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.VM;

namespace CMMMobileMaui.SCAN.CameraScan
{
    internal abstract class PartBaseScan : IScanBaseType
    {
        private readonly string prefix = "p:";
        private int? partId;
        public abstract ScanType GetScanType();

        public bool IsValid(string code)
        {
            if (code.ToLower().StartsWith(prefix))
            {
                var parsedCode = code.Replace(prefix, string.Empty);

                if (int.TryParse(parsedCode, out int id))
                {
                    partId = id;
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> ScanMethod()
        {
            var partBLL = (IPartController)API.MainObjects.Instance.ServiceProvider!.GetRequiredService(typeof(IPartController));

            var partResponse = await partBLL.GetDetailShort(new API.Contracts.v1.Requests.Part.GetPartDetailShortRequest
            {
                PartID = partId!.Value
                                    ,
                Filter = string.Empty
                                    ,
                Lang = API.MainObjects.Instance.Lang
            });

            if (partResponse.IsResponseWithData())
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    GoToPart(partResponse.Data!.First());
                });

                return true; 
            }

            return false;
        }

        public abstract void GoToPart(GetPartDetailShortResponse part);
    }
}
