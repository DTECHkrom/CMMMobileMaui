using System.Reflection;
using CMMMobileMaui.BIANOR;
using CMMMobileMaui.COMPANY.ExtraContent;

namespace CMMMobileMaui.COMPANY.BIANOR.ExtraContent
{
    public class MouldCalendarContent : CompanyExtraContent
    {
        public MouldCalendarContent() : base("sh_mc"
            , "calendar"
            , typeof(MouldCalendarChange))
        { 
        
        }
    }
}
