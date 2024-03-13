using CMMMobileMaui;
using CMMMobileMaui.COMMON;


[assembly: Dependency(typeof(SerialNumberService))]
namespace CMMMobileMaui
{
    public class SerialNumberService : ISerialNumberService
    {
        public static string SerialNumber { get; set; }

        public string GetSerialNumber() =>
            SerialNumber;
    }
}