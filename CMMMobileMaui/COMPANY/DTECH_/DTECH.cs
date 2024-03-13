using CMMMobileMaui.COMPANY.DTECH_;
using CMMMobileMaui.COMPANY.ExtraContent;

namespace CMMMobileMaui.COMPANY
{
    public class DTECH : Company
    {
        public DTECH()
        {
            AddExtraContent(new DeviceLocationContent());
            AddExtraContent(new AfterLoginContent());
        }
    }
}