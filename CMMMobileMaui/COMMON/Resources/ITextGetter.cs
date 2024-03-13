using System.Threading.Tasks;

namespace CMMMobileMaui.COMMON.Resources
{
    public interface ITextGetter
    {
        string GetText(string key);

        Task InitData(string language);
    }
}
