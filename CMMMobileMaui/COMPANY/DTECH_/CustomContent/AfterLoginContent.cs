using System.Threading.Tasks;
using CMMMobileMaui.COMPANY.ExtraContent;
using CMMMobileMaui.VIEW.AFTERLOGIN;

namespace CMMMobileMaui.COMPANY.DTECH_
{
    public class AfterLoginContent : AfterLoginExtraContent
    {
        public AfterLoginContent() : base("sh_ml", "calendar", typeof(DeviceSelectPage))
        {
        }

        public async override ValueTask<bool> ShouldOpenOnStart() =>
            false;
    }
}
