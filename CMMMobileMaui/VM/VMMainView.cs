using CommunityToolkit.Maui.Converters;
using System.Windows.Input;
using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.COMMON;
using CMMMobileMaui.SCAN;
using System.Collections.ObjectModel;
using CMMMobileMaui.API;

namespace CMMMobileMaui.VM
{
    public class VMMainView : ScannerViewModelBase
    {
        #region FIELDS

        private IDeviceController deviceCMMBLL;
        private DBMain.Engine en = new DBMain.Engine();
        private bool isFileHistoryVisible;

        #endregion

        #region COMMAND ShowDeviceCommand

        public ICommand ShowDeviceCommand
        {
            get;
        }

        #endregion

        #region COMMAND AddWorkOrderCommand

        public ICommand AddWorkOrderCommand
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

        #region PROPERTY CurrentDeviceHistory

        public MODEL.DeviceHistoryModel? CurrentDeviceHistory
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

        public ObservableCollection<DBMain.Model.History> FilesHistoryList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY IsFileHistoryVisible

        public bool IsFileHistoryVisible
        {
            get => isFileHistoryVisible;
            set => SetProperty(ref isFileHistoryVisible, value);
        }

        #endregion

        #region Cstr

        public VMMainView(IDeviceController deviceCMMBLL)
        {
            this.deviceCMMBLL = deviceCMMBLL;
            HistoryList = new ObservableCollection<MODEL.DeviceHistoryModel>();
            FilesHistoryList = new ObservableCollection<DBMain.Model.History>();

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
                    if (obj is MODEL.DeviceHistoryModel histModel)
                    {
                        histModel.IsBusy = true;
                        histModel.BaseItem.Mod_Date = DateTime.Now;                     

                        int a = await en.UpdateHist(histModel.BaseItem);

                        if (histModel.BaseItem.Type == "d")
                        {
                            ShowDevice(histModel);

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
                    if (obj is DBMain.Model.History histModel)
                    {
                        histModel.Mod_Date = DateTime.Now;

                        int a = await en.UpdateHist(histModel);

                        var fileList = await en.GetAllFilesHistory(API.MainObjects.Instance.CurrentUser!.PersonID);
                        FilesHistoryList = new ObservableCollection<DBMain.Model.History>(fileList.Distinct().ToList());
                        IsFileHistoryVisible = FilesHistoryList.Count > 0;
                        OnPropertyChanged(nameof(FilesHistoryList));
                        ShowFileItem(histModel);
                    }
                }
            });

            AddWorkOrderCommand = new Command(async (obj) =>
            {
                if (CanClick())
                {
                    if (obj is MODEL.DeviceHistoryModel histModel)
                    {
                        histModel.IsBusy = true;
                        histModel.BaseItem.Mod_Date = DateTime.Now;

                        int a = await en.UpdateHist(histModel.BaseItem);

                        if (histModel.BaseItem.Type == "d")
                        {
                            OpenWOItem(histModel);

                            await Task.Delay(100);

                            InitDeviceHistoryList();
                        }
                    }
                }
            });
        }

        #endregion

        #region METHOD OpenWOItem

        private async void OpenWOItem(MODEL.DeviceHistoryModel histModel)
        {
            if(histModel.CurrentDevice != null)
            {
                MainObjects.Instance.CurrentDevice = histModel.CurrentDevice;
                await OpenModalPage(new VIEW.WorkOrderSingleView(null));           
            }
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
                    var device = new MODEL.DeviceHistoryModel(tt
                        , ShowDeviceCommand
                        , AddWorkOrderCommand);

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

                    hist = new MODEL.DeviceHistoryModel(histOrg
                                            , ShowDeviceCommand
                                            , AddWorkOrderCommand);
                }

                hist.CurrentDevice = item;
                InitDeviceHistoryList();

                ShowDevice(hist);
            }
        }

        #endregion

        #region METHOD ShowItem

        private async void ShowDevice(MODEL.DeviceHistoryModel histModel)
        {
            if(histModel.CurrentDevice == null)
            { 
                CurrentDeviceHistory = histModel;

                if (CurrentDeviceHistory.BaseItem.Type == "d")
                {
                    var deviceResponse = await deviceCMMBLL.Get(new API.Contracts.v1.Requests.Device.GetDeviceByIDLangRequest
                    {
                        MachineID = CurrentDeviceHistory.BaseItem.ID
                    ,
                        Lang = API.MainObjects.Instance.Lang
                    });

                    if (deviceResponse.IsResponseWithData(this))
                    {
                        await OpenModalPage(new VIEW.DeviceView(deviceResponse.Data!));
                    }
                }
            }
            else
            {
                await OpenModalPage(new VIEW.DeviceView(histModel.CurrentDevice));
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
            FilesHistoryList = new ObservableCollection<DBMain.Model.History>(fileList.Distinct().ToList());

            IsFileHistoryVisible = FilesHistoryList.Count > 0;

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

        public override string GetVisualScanPresentation() => "precision_manufacturing";

        #endregion
    }
}
