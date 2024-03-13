using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.COMMON
{
    public interface IViewLocationFetcher
    {
        System.Drawing.PointF GetCoordinates(VisualElement view);
    }
}
