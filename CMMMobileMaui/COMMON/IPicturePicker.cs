using System.IO;
using System.Threading.Tasks;

namespace CMMMobileMaui.COMMON
{
    public interface IPicturePicker
    {
        Task<Stream> GetImageStreamAsync();
    }
}
