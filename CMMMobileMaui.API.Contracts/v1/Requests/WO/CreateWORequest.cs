using System;

namespace CMMMobileMaui.API.Contracts.v1.Requests.WO
{
    public class CreateWORequest
    {
        public int MachineID
        {
            get;
            set;
        }

        public short PersonID
        {
            get;
            set;
        }

        public int? CategoryID
        {
            get;
            set;
        }

        public int LevelID
        {
            get;
            set;
        }

        public int? ReasonID
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public int? DepartmentID
        {
            get;
            set;
        }

        public short? AssignedPersonID
        {
            get;
            set;
        }

        public string Location
        {
            get;
            set;
        }

        public DateTime? Start_Date
        {
            get;
            set;
        }

        public DateTime? End_Date
        {
            get;
            set;
        }

        public byte[] DevImage
        {
            get;
            set;
        }
    }
}
