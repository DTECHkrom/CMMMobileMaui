using CMMMobileMaui.COMPANY.ExtraContent;
using CMMMobileMaui.COMPANY.PUK_.CustomContent;

namespace CMMMobileMaui.COMPANY.PUK_
{
    public class PUK : Company
    {
        public PUK()
        {
            AddExtraContent(new DeviceSelectContent());
            AddExtraContent(new DeviceLocationContent());
        }
    }
}
