namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceSetView : PageBase<VM.VMDeviceSet>
    {
        #region Cstr

        public DeviceSetView()
        {
            InitializeComponent();

            ViewModel.OnItemsSet += _vmMain_OnItemsSet;
            ViewModel.OnCollapseExpandAll += ViewModel_OnCollapseExpandAll;
        }

        private void ViewModel_OnCollapseExpandAll(object? sender, bool e)
        {
            if(e)
            {
                treeView.ExpandAll();
            }
            else
            {
                treeView.CollapseAll();
            }
        }

        private void _vmMain_OnItemsSet(object? sender, List<COMMON.DeviceSetModel> e)
        {
            foreach (var item in e)
            {
                treeView.Collapse(item);
            }
        }

        #endregion
    }
}