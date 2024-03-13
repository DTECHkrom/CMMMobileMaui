using Mopups.Pages;
using Mopups.Services;
using System.Windows.Input;

namespace CMMMobileMaui.CUST
{
    public enum CustomMessageType
    {
        Dialog
            , Info
            , Error
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomMessageDialog : PopupPage
    {
        private static Task responseTask;
        private static bool responseResult = false;
        private static bool isTaskRun = false;

        public static BindableProperty MessageDialogTypeProperty = BindableProperty.Create(nameof(DialogType), typeof(CustomMessageType), typeof(CustomMessageDialog));

        public CustomMessageType DialogType
        {
            get
            {
                return (CustomMessageType)GetValue(MessageDialogTypeProperty);
            }

            set
            {
                SetValue(MessageDialogTypeProperty, value);
            }
        }

        public ICommand CloseCommand
        {
            get;
        }

        public ICommand ConfirmCommand
        {
            get;
        }

        public static async Task<bool> Show(CustomMessageType type, string msg)
        {
            var window = new CustomMessageDialog(type, msg);
            isTaskRun = true;
            responseTask = new Task(() =>
            {
                while (true)
                {
                    if (isTaskRun == false)
                    {
                        break;
                    }
                }
            });

            await MopupService.Instance.PushAsync(window);

            if (responseTask.Status != TaskStatus.Running)
            {
                responseTask.Start();
            }

            await Task.WhenAny(responseTask);

            return responseResult;
        }

        private CustomMessageDialog(CustomMessageType type, string msg)
        {
            InitializeComponent();

            DialogType = type;

            CloseCommand = new Command((obj) =>
            {
                responseResult = false;
                isTaskRun = false;
                MopupService.Instance.PopAsync();
            });

            ConfirmCommand = new Command((obj) =>
            {
                responseResult = true;
                isTaskRun = false;

                if (MopupService.Instance.PopupStack.Count > 0)
                    MopupService.Instance.PopAsync();
            });

            if (DialogType != CustomMessageType.Dialog)
            {
                btnMsgCancel.IsVisible = false;
            }

            this.BindingContext = this;

            lblCustomText.Text = msg;
        }
    }
}