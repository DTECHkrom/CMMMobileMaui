using CommunityToolkit.Maui.Converters;
using System.Windows.Input;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.COMMON;
using CMMMobileMaui.SCAN;
using System.Collections.ObjectModel;

namespace CMMMobileMaui.VM
{
    public class VMMainView : ScannerViewModelBase
    {
        #region FIELDS

        private IDeviceController deviceCMMBLL;
        private DBMain.Engine en = new DBMain.Engine();

        #endregion

        #region COMMAND ShowDeviceCommand

        public ICommand ShowDeviceCommand
        {
            get;
        }

        #endregion

        #region COMMAND ShowFileCommand

        public ICommand ShowFileCommand
        {
            get;
        }

        #endregion

        #region PROPERTY CurrentHistory

        public MODEL.DeviceHistoryModel? CurrentHistory
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CurrentFileHistory

        public DBMain.Model.History? CurrentFileHistory
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY HistoryList

        public ObservableCollection<MODEL.DeviceHistoryModel> HistoryList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY FilesHistoryList

        public List<DBMain.Model.History>? FilesHistoryList
        {
            get;
            set;
        }

        #endregion

        #region Cstr

        public VMMainView(IDeviceController deviceCMMBLL)
        {
            this.deviceCMMBLL = deviceCMMBLL;
            HistoryList = new ObservableCollection<MODEL.DeviceHistoryModel>();

            //SConsts.SetGlobalAction(SConsts.DEV_HIST_REF, async () =>
            //{
            //    //DeviceState = LayoutState.Loading;

            //    //HistoryList.Clear();

            //    //DBMain.Engine en = new DBMain.Engine();
            //    //CUST.DeviceHistViewCell.DisplayIndex = 0;

            //    //var histList = await en.GetAllHistory(CMMMobileMaui.API.MainObjects.Instance.CurrentUser.PersonID);
            //    //HistoryList.AddRange(histList.Distinct().ToList());

            //    //DeviceState = LayoutState.None;

            //});

            ShowDeviceCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (obj != null && obj
                    is MODEL.DeviceHistoryModel histModel)
                    {
                        histModel.IsBusy = true;
                        histModel.BaseItem.Mod_Date = DateTime.Now;                     

                        int a = await en.UpdateHist(histModel.BaseItem);

                        if (histModel.BaseItem.Type == "d")
                        {
                            ShowItem(obj);

                            await Task.Delay(100);

                            InitDeviceHistoryList();
                        }
                    }
                }
            });

            ShowFileCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (obj != null && obj
                    is DBMain.Model.History histModel)
                    {
                        histModel.Mod_Date = DateTime.Now;

                        int a = await en.UpdateHist(histModel);

                        var fileList = await en.GetAllFilesHistory(API.MainObjects.Instance.CurrentUser!.PersonID);
                        FilesHistoryList = fileList.Distinct().ToList();

                        OnPropertyChanged(nameof(FilesHistoryList));
                        ShowFileItem(histModel);
                    }
                }
            });
        }

        #endregion

        #region METHOD InitDeviceHistoryList

        private async void InitDeviceHistoryList()
        {
            HistoryList.Clear();

            var histList = await en.GetAllHistory(API.MainObjects.Instance.CurrentUser!.PersonID);

            if (histList != null
            && histList.Count > 0)
            {
                histList.ForEach(tt =>
                {
                    var device = new MODEL.DeviceHistoryModel(tt, ShowDeviceCommand);

                    HistoryList.Add(device);
                });
            }
        }

        #endregion

        #region METHOD ShowFileItem

        private async void ShowFileItem(DBMain.Model.History hist)
        {
            var ext = Path.GetExtension(hist.Name);

            if (!string.IsNullOrEmpty(ext))
            {
                var file = await en.GetSingleFileHistory(API.MainObjects.Instance.CurrentUser!.PersonID, hist.Name);

                if (file != null)
                {
                    var fileResponse = await deviceCMMBLL.GetFile(new API.Contracts.v1.Requests.GetFileNameRequest
                    {
                        FileName = hist.Name
                    });

                    if (fileResponse.IsResponseWithData(this))
                    {
                        await SaveBinary(file.Name, fileResponse.Data!.FileData);
                    }
                }

                //if (ext.ToLower() == ".pdf")
                //{
                //    var fileResponse = await deviceCMMBLL.GetFile(new CMMMobileMaui.API.Contracts.v1.Requests.GetFileNameRequest
                //    {
                //        FileName = hist.Name
                //    });

                //    if (fileResponse.IsValid)
                //    {
                //        string fileName = hist.Name.Substring(hist.Name.LastIndexOf(@"\") + 1, hist.Name.Length - hist.Name.LastIndexOf(@"\") - 1);

                //        if (!string.IsNullOrEmpty(fileName))
                //        {
                //            SaveBinary(fileName, fileResponse.Data.FileData);
                //        }
                //    }
                //}
                //else if (ext.ToLower() == ".jpg")
                //{
                //    var fileResponse = await deviceCMMBLL.GetFile(new CMMMobileMaui.API.Contracts.v1.Requests.GetFileNameRequest
                //    {
                //        FileName = hist.Name
                //    });

                //    if (fileResponse.IsValid)
                //    {
                //        await OpenModalPage(new VIEW.ImageView(fileResponse.Data.FileData));
                //    }
                //}
            }
        }

        #endregion

        #region METHOD SaveBinary

        private async Task<string> SaveBinary(string filename, byte[] bytes)
        {
            string filepath = GetFilePath(filename);
            try
            {
                if (File.Exists((filepath)))
                {
                    File.Delete(filepath);
                }

                File.WriteAllBytes(filepath, bytes);

                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(filepath)
                });
            }
            catch (Exception) { }

            return filepath;
        }

        #endregion

        #region METHOD GetFilePath

        private string GetFilePath(string filename)
        {
            return Path.Combine(GetDocsPath(), filename);
        }

        #endregion

        #region METHOD GetDocsPath

        string GetDocsPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        #endregion

        #region METHOD GoToDevice

        private async void GoToDevice(GetDeviceListResponse item)
        {
            if (item != null)
            {
                var hist = HistoryList.FirstOrDefault(tt => tt.BaseItem.ID == item.MachineID
                && tt.BaseItem.PersonID == API.MainObjects.Instance.CurrentUser!.PersonID
                && tt.BaseItem.Type == "d");

                if (hist != null)
                {
                    hist.BaseItem.Mod_Date = DateTime.Now;
                    int a = await en.UpdateHist(hist.BaseItem);
                }
                else
                {
                    var histOrg = new DBMain.Model.History();
                    histOrg.ID = item.MachineID;
                    histOrg.Type = "d";
                    histOrg.Mod_Date = DateTime.Now;
                    histOrg.Name = item.AssetNo;
                    histOrg.PersonID = API.MainObjects.Instance.CurrentUser!.PersonID;
                    int b = await en.InsertHist(histOrg);
                }

                InitDeviceHistoryList();

                ShowItem(hist);
            }
        }

        #endregion

        #region METHOD ShowItem

        private async void ShowItem(object? obj)
        {
            if (obj is MODEL.DeviceHistoryModel histModel)
            {
                CurrentHistory = histModel;

                if (CurrentHistory.BaseItem.Type == "d")
                {
                    var deviceResponse = await deviceCMMBLL.Get(new API.Contracts.v1.Requests.Device.GetDeviceByIDLangRequest
                    {
                        MachineID = CurrentHistory.BaseItem.ID
                    ,
                        Lang = API.MainObjects.Instance.Lang
                    });

                    if (deviceResponse.IsResponseWithData(this))
                    {
                        await OpenModalPage(new VIEW.DeviceView(deviceResponse.Data!));
                    }
                }
            }
            else if (obj is GetDeviceListResponse dev)
            {
                await OpenModalPage(new VIEW.DeviceView(dev));
            }         
        }

        #endregion

        #region METHOD SetMainHistoryList

        public void SetMainHistoryList()
        {
            if (API.MainObjects.Instance.CurrentUser != null)
            {
                InitDeviceHistoryList();
                InitFilesHistoryList();
            }
        }

        #endregion

        #region METHOD InitFilesHistoryList

        private async void InitFilesHistoryList()
        {
            if (FilesHistoryList != null)
            {
                FilesHistoryList.Clear();
            }

            var fileList = await en.GetAllFilesHistory(API.MainObjects.Instance.CurrentUser!.PersonID);
            FilesHistoryList = fileList.Distinct().ToList();

            en.CloseConnection();
            OnPropertyChanged(nameof(FilesHistoryList));
        }

        #endregion

        #region METHOD GetScanItems

        public override IEnumerable<IScanType> GetScanItems()
        {
            List<IScanType> scanTypes = new List<IScanType>();

            var device = new DeviceScan();

            device.UIMethod = (obj) =>
            {
                if (obj is GetDeviceListResponse dev)
                {
                    GoToDevice(dev);
                }
            };

            scanTypes.Add(device);

            return scanTypes;
        }

        #endregion
    }
}
