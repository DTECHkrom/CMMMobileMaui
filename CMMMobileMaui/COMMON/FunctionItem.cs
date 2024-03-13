using System.Windows.Input;

namespace CMMMobileMaui.COMMON
{
    public class FunctionItem
    {
        public DisplayImage ImageInfo
        {
            get;
            set;
        }

        public ICommand Command
        {
            get;
            set;
        }
    }
}
