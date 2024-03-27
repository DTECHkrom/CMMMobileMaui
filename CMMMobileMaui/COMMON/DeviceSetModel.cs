using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace CMMMobileMaui.COMMON
{
    public enum LinePartType
    {
        Branch,
        Location,
        Set,
        Device
    }

    public class DeviceSetModel : ObservableObject
    {
        #region PROPERTY

        public int ID
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY Name

        private string name;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        #endregion

        #region PROPERTY LineType

        public LinePartType LineType
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY FontColor

        private string fontColor = "Black";

        public string FontColor
        {
            get => fontColor;
            set => SetProperty(ref fontColor, value);
        }

        #endregion

        #region PROPERTY IsFiltered

        private bool isFiltered;
        public bool IsFiltered
        {
            get => isFiltered;
            set => SetProperty(ref isFiltered, value);
        }

        #endregion

        #region PROPERTY StateID

        private int stateID;

        public int StateID
        {
            get
            {
                return stateID;
            }

            set
            {
                stateID = value;

                FontColor = SConsts.GetStateColor(stateID);

                if (stateID == 1)
                {
                    IconColor = Colors.Green;
                }
                else if (stateID == 2)
                {
                    IconColor = Colors.Yellow;

                }
                else if (stateID == 3)
                {
                    if (IsCritical)
                    {
                        IconColor = Colors.Blue;
                    }
                    else
                    {
                        IconColor = Colors.Red;
                    }
                }
            }
        }

        #endregion

        #region PROPERTY IconColor

        private Color iconColor = Colors.Transparent;
        public Color IconColor
        {
            get => iconColor;
            set => SetProperty(ref iconColor, value);
        }

        #endregion

        #region PROPERTY IsCritical

        private bool isCritical;
        public bool IsCritical
        {
            get => isCritical;
            set => SetProperty(ref isCritical, value);
        }

        #endregion

        #region PROPERTY Children

        private ObservableCollection<DeviceSetModel> children = new ObservableCollection<DeviceSetModel>();

        public ObservableCollection<DeviceSetModel> Children
        {
            get => children;
            set => SetProperty(ref children, value);
        }

        #endregion

        #region PROPERTY Level

        public int Level
        {
            get;
            set;
        }

        #endregion

        #region Cstr
        private DeviceSetModel()
        {
        }

        #endregion

        #region METHOD HasChildren

        public bool HasChildren() =>
            Children != null && Children.Count > 0;

        #endregion

        #region METHOD IsFilerItem

        public bool IsFilerItem(string filter)
        {
            IsFiltered = false;

            if (string.IsNullOrWhiteSpace(filter))
            {
                return true;
            }

            bool isFilter = false;

            if (Name.ToLower().Contains(filter.ToLower()))
            {
                IsFiltered = true;
                isFilter = true;
            }

            foreach (var tt in Children)
            {
                tt.IsFiltered = false;

                if (tt.Name.ToLower().Contains(filter.ToLower()))
                {
                    tt.IsFiltered = true;
                    isFilter = true;
                }
            }

            return isFilter;
        }

        #endregion

        #region METHOD AddChild

        public void AddChild(DeviceSetModel child)
        {
            Children.Add(child);

            if (LineType == LinePartType.Set)
            {
                if (child.IsCritical
                    && child.StateID == 3)
                {
                    FontColor = child.FontColor;
                    IconColor = Colors.Blue;
                }
            }
        }

        #endregion

        #region METHOD SetNodeIcon

        public void SetNodeIcon()
        {
            if (Children.Count > 0)
            {
                var items = from dd in Children
                            group dd by new { dd.StateID, dd.FontColor, dd.IconColor } into g
                            select new { g.Key.StateID, g.Key.FontColor, g.Key.IconColor, count = g.Count() };

                var item = items.OrderByDescending(tt => tt.count).FirstOrDefault();

                if (item is null)
                    return;

                IconColor = item.IconColor;
                FontColor = item.FontColor;

                return;
            }
        }

        #endregion

        public static DeviceSetModel FromBranchName(string branchName)
        {
            DeviceSetModel deviceSetModel = new DeviceSetModel();
            deviceSetModel.Name = branchName;
            deviceSetModel.LineType = LinePartType.Branch;
            return deviceSetModel;
        }

        public static DeviceSetModel FromLocationName(string locationName)
        {
            DeviceSetModel deviceSetModel = new DeviceSetModel();
            deviceSetModel.Name = locationName;
            deviceSetModel.LineType = LinePartType.Location;
            return deviceSetModel;
        }

        public static DeviceSetModel FromSet(string setName, int setID)
        {
            DeviceSetModel deviceSetModel = new DeviceSetModel();
            deviceSetModel.ID = setID;
            deviceSetModel.Name = setName;
            deviceSetModel.LineType = LinePartType.Set;

            return deviceSetModel;
        }

        public static DeviceSetModel FromDevice(GetDeviceListResponse device)
        {
            DeviceSetModel deviceSetModel = new DeviceSetModel();
            deviceSetModel.ID = device.MachineID;
            deviceSetModel.Name = device.AssetNo;
            deviceSetModel.LineType = LinePartType.Device;
            deviceSetModel.IsCritical = device.IsCritical.HasValue ? device.IsCritical.Value : false;
            deviceSetModel.StateID = device.StateID;

            return deviceSetModel;
        }
    }
}
