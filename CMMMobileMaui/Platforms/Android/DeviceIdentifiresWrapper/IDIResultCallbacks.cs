namespace CMMMobileMaui.DeviceIdentifiresWrapper
{
    public interface IDIResultCallbacks
    {
        void OnSuccess(string message);
        void OnError(string message);
        void OnDebugStatus(string message);
        void OnResult(string message);
    }
}