using CMMMobileMaui.SCAN;
using CMMMobileMaui.VM;
using ZXing.Net.Maui;
//using Camera.MAUI;
//using Camera.MAUI.ZXingHelper;
//using Camera.MAUI.ZXing;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanView : PageBase<VM.VMScan>
    {

        #region Cstr

        public ScanView()
        {
            InitializeComponent();

            //var possibleFormats = new List<BarcodeFormat>
            //{
            //    BarcodeFormat.Code128,
            //    BarcodeFormat.QrCode,
            //    BarcodeFormat.Ean13,
            //    BarcodeFormat.DataMatrix
            //};

            // codeScanner.BarcodeDetected += CodeScanner_BarcodeDetected;
            //  codeScanner.BarCodeDecoder = new ZXingBarcodeDecoder();
            codeScanner.BarcodesDetected += CodeScanner_BarcodesDetected;
            codeScanner.Options = new BarcodeReaderOptions() 
            { 
                Formats = BarcodeFormat.Ean13 | BarcodeFormat.QrCode | BarcodeFormat.Code128,
                TryHarder = true,
                AutoRotate = true,
                TryInverted = true,
            };

            //codeScanner.BarCodeDetectionFrameRate = 10;
            //codeScanner.BarCodeDetectionMaxThreads = 5;
            //codeScanner.ControlBarcodeResultDuplicate = true;
            //codeScanner.BarCodeDetectionEnabled = true;
        }

        private void CodeScanner_BarcodesDetected(object? sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
        {
            Dispatcher.Dispatch(()=>
                { 
                           
                    if(e.Results == null || e.Results.Length == 0)
                    {
                        return;
                    }

                ViewModel.ScanMethod(e.Results[0]);
            });
        }

        public ScanView(int id):this((VM.ScanType)id)
        {
        }

        public ScanView(VM.ScanType scan) : this(new List<VM.ScanType> { scan })
        {
        }

        public ScanView(IEnumerable<ScanType> scanTypes) : this()
        {
            ViewModel.SetScanType(scanTypes);
        }

        public ScanView(IEnumerable<IScanType> scanTypes, string icon) : this()
        {
            ViewModel.SetScanType(scanTypes, icon);
        }

        //private void CodeScanner_BarcodeDetected(object sender, BarcodeEventArgs args)
        //{
        //    if (args.Result == null)
        //        return;

        //    ViewModel.ScanMethod(args.Result[0]);
        //}

        #endregion

        private void codeScanner_CamerasLoaded(object? sender, EventArgs e)
        {
            InitFirstCamera();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if(codeScanner.Camera != null)
            //{             
            //    await codeScanner.StartCameraAsync(codeScanner.Camera!.AvailableResolutions[0]);
            //    ViewModel.IsScanning = true;
            //}
        }

        private async void InitFirstCamera()
        {
            //if (codeScanner.Cameras.Count > 0)
            //{
            //    codeScanner.Camera = codeScanner.Cameras.FirstOrDefault();
            //    await Task.Delay(100);

            //    await codeScanner.StopCameraAsync();
            //    await codeScanner.StartCameraAsync(GetResulutionForCamera());

            //    codeScanner.BarCodeDetectionEnabled = true;

            //    ViewModel.IsScanning = true;
            //}

           codeScanner.CameraLocation = CameraLocation.Rear;

        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
          //  await codeScanner.StopCameraAsync();
        }

        private Size GetResulutionForCamera() => 
            new Size(DeviceDisplay.Current.MainDisplayInfo.Height, DeviceDisplay.Current.MainDisplayInfo.Width);

    }
}