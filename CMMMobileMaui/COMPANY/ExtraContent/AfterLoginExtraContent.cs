using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMMMobileMaui.COMPANY.ExtraContent
{
    public abstract class AfterLoginExtraContent : CompanyExtraContent
    {
      //  private readonly string routePath = "//AfterLoginPage";

        protected AfterLoginExtraContent(string textKey, string icon, Type contentTemplateType) : base(textKey, icon, "AfterLoginPage", contentTemplateType)
        {
        }
        public abstract ValueTask<bool> ShouldOpenOnStart();
    }
}
