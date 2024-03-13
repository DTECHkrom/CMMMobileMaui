using CMMMobileMaui.API.Contracts;
using System.Collections.ObjectModel;

namespace CMMMobileMaui.COMMON.PictureOperation
{
    public interface IPictureModel
    {
        ObservableCollection<WOFile> PictureList { get; set; }

        bool IsPicture
        {
            get;
            set;
        }

        bool CanEdit
        {
            get;
        }
    }
}
