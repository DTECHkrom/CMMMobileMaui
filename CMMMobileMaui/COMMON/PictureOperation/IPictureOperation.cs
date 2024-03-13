using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts;

namespace CMMMobileMaui.COMMON.PictureOperation
{
    public interface IPictureOperation
    {
        Task<bool> Insert(WOFile fileModel);
        Task<bool> Delete(WOFile fileModel);

        Task<bool> Replace(WOFile fileModel);

        Task Select();

        IPictureModel PictureModel
        {
            get;
        }
    }
}
