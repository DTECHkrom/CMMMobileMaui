using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.COMMON
{
    public interface IDownloadVersion
    {
        event EventHandler<bool> OnStopInstallation;

        void DownloadApp(string version, VisualElement pbar, bool isHttps);

        void CancelDownload();
    }
}
