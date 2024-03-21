using CMMMobileMaui.SCAN;
using CMMMobileMaui.SCAN.CameraScan;
using System.Windows.Input;
using ZXing.Net.Maui;

namespace CMMMobileMaui.VM
{
    public enum ScanType
    {
        Part = 0
        , Device
        , Person
        , PersonSett
        , Text
        , DevText
        , PartText
        , PartInv
        , PartGiver
        , DeviceMain
    }

    public class VMScan : COMMON.ViewModelBase
    {
        private IEnumerable<ScanType>? _scanTypes;
        private IEnumerable<IScanType>? _zebraScanTypes;
        private readonly Color defaultIconColor = Colors.White;

        private Action<string>? scanAction;

        #region PROPERTY IsScanning

        private bool isScanning;

        public bool IsScanning
        {
            get => isScanning;
            set
            {
                SetProperty(ref isScanning, value);
                IsBusy = !value;
            }
        }

        #endregion

        #region PROPERTY Icon

        private string icon;

        public string Icon
        {
            get => icon;
            set => SetProperty(ref icon, value);
        }

        #endregion

        #region PROPERTY IconColor

        private Color iconColor;

        public Color IconColor
        {
            get => iconColor;
            set => SetProperty(ref iconColor, value);
        }

        #endregion

        #region COMMAND ScanResultCommand

        public ICommand? ScanResultCommand
        {
            get;
        }

        #endregion

        #region Cstr

        public VMScan()
        {
            IsScanning = true;
        }

        #endregion

        #region METHOD ScanMethod
        internal void ScanMethod(BarcodeResult scanResult)
        {
            if (!string.IsNullOrEmpty(scanResult.Value))
            {
                IsScanning = false;
                scanAction?.Invoke(scanResult.Value);
            }
        }

        #endregion

        #region METHOD SetScanType

        private async void ScanByScanType(string scanResult)
        {
            if(_scanTypes is null)
            {
                return;
            }

            foreach (var scanType in _scanTypes)
            {
                var scanMethod = CameraScanManager.Instance.GetScanMethod(scanType);

                if (scanMethod is not null)
                {
                    if (scanMethod.IsValid(scanResult))
                    {
                        var result = await scanMethod.ScanMethod();

                        if (!result)
                        {
                            IsScanning = true;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        IconColor = Colors.Red;
                    }
                }
            }

            ChangeIconColorToDefaultAfterDelay();
        }

        #endregion

        #region METHOD ChangeIconColorToDefaultAfterDelay

        private void ChangeIconColorToDefaultAfterDelay()
        {
            Task.Run(async () =>
            {
                await Task.Delay(1000);
                IconColor = defaultIconColor;
            });
        }

        #endregion

        #region METHOD SetScanType

        public void SetScanType(IEnumerable<ScanType> scanTypes)
        {
            _scanTypes = scanTypes;
            scanAction = ScanByScanType;
        }

        #endregion

        #region METHOD SetScanType

        public void SetScanType(IEnumerable<IScanType> scanTypes
            , string icon)
        {
            Icon = icon;
            IconColor = defaultIconColor;
            _zebraScanTypes = scanTypes;
            scanAction = ScanByZebraScanType;
        }

        #endregion

        #region METHOD ScanByZebraScanType

        private async void ScanByZebraScanType(string scanResult)
        {
            if(_zebraScanTypes is null)
            {
                return;
            }   

            foreach (var scanType in _zebraScanTypes)
            {
                if (scanType.IsValid(scanResult))
                {
                    await Shell.Current.Navigation.PopAsync();

                    await scanType.ScanMethod();

                    break;
                }   
                else
                {
                    IconColor = Colors.Red;
                }
            }

            IsScanning = true;
            ChangeIconColorToDefaultAfterDelay();
        }

        #endregion
    }
}