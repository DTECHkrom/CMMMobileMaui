using System;
using System.Collections.Generic;
using System.Text;
using CMMMobileMaui.VIEW;

namespace CMMMobileMaui.COMPANY.ExtraContent
{
    public class DeviceLocationContent : CompanyExtraContent
    {
        public DeviceLocationContent() 
            : base("sh_ml"
                  , "location"
                  , typeof(DeviceLocationManagePage))
        {
        }
    }
}
