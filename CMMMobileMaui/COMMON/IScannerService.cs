using System;

namespace CMMMobileMaui.COMMON
{
    public interface IScannerService
    {
        //event EventHandler<OnBarcodeScannedEventArgs> OnBarcodeScanned;
        //Action<OnBarcodeScannedEventArgs> ItemScanned
        //{
        //    get;
        //    set;
        //}

        Action<string> ItemScanned
        {
            get;
            set;
        }
    }

    //public class OnBarcodeScannedEventArgs : EventArgs
    //{
    //    public IEnumerable<ScannedBarCode> BarCodes { get; set; }
    //    public OnBarcodeScannedEventArgs(IEnumerable<ScannedBarCode> barcodes)
    //    {
    //        BarCodes = barcodes;
    //    }
    //}

    //public class ScannedBarCode
    //{
    //    public string Barcode { get; }
    //    public string Symbology { get; }

    //    public ScannedBarCode(string barcode, string symbology)
    //    {
    //        this.Barcode = barcode;
    //        this.Symbology = symbology;
    //    }
    //}
}
