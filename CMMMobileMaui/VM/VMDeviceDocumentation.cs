using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Contracts.v1.Responses;
using CMMMobileMaui.API.Interfaces;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.VM
{
    public class VMDeviceDocumentation: COMMON.ViewModelBase
    {
        #region FIELDS

        private string _path;
        private List<string> _prevList = new List<string>();

        #endregion

        #region PROPERTY DocsList

        public List<GetDirectoryFilesResponse> DocsList
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CurrentDoc

        public GetDirectoryFilesResponse CurrentDoc
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY CurrentPath

        public string CurrentPath
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY ISPrevEnable

        public bool ISPrevEnable
        {
            get;
            set;
        }

        #endregion

        #region COMMAND OpenDocCommand

        public ICommand OpenDocCommand
        {
            get;
        }

        #endregion

        #region COMMAND MoveBackCommand

        public ICommand MoveBackCommand
        {
            get;
        }

        private IDeviceController deviceCMMBLL;

        #endregion

        #region COMMAND HomeCommand

        public ICommand HomeCommand
        {
            get;
        }

        #endregion

        #region Cstr

        public VMDeviceDocumentation(IDeviceController deviceCMMBLL)
        {
            this.deviceCMMBLL = deviceCMMBLL;

            HomeCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    ISPrevEnable = false;
                    _prevList = new List<string>();
                    SetStartData(_path);
                }
            });

            MoveBackCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    var str = _prevList.LastOrDefault();

                    if (!string.IsNullOrEmpty(str))
                    {
                        _prevList.Remove(str);
                        CheckPrevList();
                        SetStartData(str);
                    }
                }
            });

            OpenDocCommand = new Command((obj) =>
            {
                if (CanClick())
                {
                    if (obj != null)
                    {
                        CurrentDoc = obj as GetDirectoryFilesResponse;
                    }

                    if (CurrentDoc != null)
                    {
                        ShowCurrentDoc();
                    }
                }
            });
        }

        #endregion

        #region METHOD ShowCurrentDoc

        private async void ShowCurrentDoc()
        {
            if (CurrentDoc.IsDir)
            {
                _prevList.Add(CurrentPath);
                CheckPrevList();
                SetStartData(CurrentDoc.FullName);
            }
            else
            {
                var ext = Path.GetExtension(CurrentDoc.FullName);
              
                if (!string.IsNullOrEmpty(ext))
                {
                    DBMain.Engine en = new DBMain.Engine();

                    var file = await en.GetSingleFileHistory(API.MainObjects.Instance.CurrentUser.PersonID, CurrentDoc.FullName);

                    if (file == null)
                    {
                        DBMain.Model.History hist = new DBMain.Model.History();

                        if (API.MainObjects.Instance.CurrentDevice != null)
                        {
                            hist.Description = API.MainObjects.Instance.CurrentDevice.AssetNo;
                        }

                        hist.Mod_Date = DateTime.Now;
                        hist.Name = CurrentDoc.FullName;
                        hist.Type = "f";
                        hist.PersonID = API.MainObjects.Instance.CurrentUser.PersonID;

                        await en.InsertHist(hist);
                    }
                    else
                    {
                        file.Mod_Date = DateTime.Now;

                        await en.UpdateHist(file);
                    }

                    en.CloseConnection();

                    if (ext.ToLower() == ".pdf" || ext.ToLower() == ".jpg")
                    {
                        var fileResponse = await deviceCMMBLL.GetFile(new API.Contracts.v1.Requests.GetFileNameRequest
                        {
                            FileName = CurrentDoc.FullName
                        });

                        if (fileResponse.IsValid)
                        {
                            await SaveBinary(CurrentDoc.Name, fileResponse.Data.FileData);
                        }
                    }
                }
            }
        }

        #endregion

        #region METHOD CheckPrevList

        private void CheckPrevList()
        {
            ISPrevEnable = _prevList.Count > 0;
            OnPropertyChanged("ISPrevEnable");
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
                // await OpenModalPage(COMMON.SConsts.GetBaseNavigationPage(new VIEW.WebViewPageCS(filepath)));

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

        #region METHOD SetBasePath

        public void SetBasePath(string path)
        {
            _path = path;
        }

        #endregion

        #region METHOD SetStartData

        public async void SetStartData(string path)
        {
            CurrentPath = path;
          //  this._path = path;
            var docsResponse = await deviceCMMBLL.GetDirFiles(new API.Contracts.v1.Requests.GetDirectoryFilesRequest
            {
                DirectoryPath = path
            });

            if (docsResponse.IsValid)
            {
                DocsList = docsResponse.Data.ToList();
                OnPropertyChanged("DocsList");
                OnPropertyChanged("CurrentPath");
            }
        }

        #endregion
    }
}
