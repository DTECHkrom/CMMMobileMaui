using System;
using System.Collections.Generic;
using CMMMobileMaui.API.Contracts.Models.COMMON;

namespace CMMMobileMaui.API.Contracts.v1.Responses.WO
{
    public class GetWOsResponse
    {
        private string take_Persons;

        public int WorkOrderID
        {
            get;
            set;
        }

        public int MachineID
        {
            get;
            set;
        }

        [CanSortItem]
        public string Asset_No
        {
            get;
            set;
        }

        [CanSortItem]
        public string Device_Category
        {
            get;
            set;
        }

        public string WO_Desc
        {
            get;
            set;
        }

        public string WO_Category
        {
            get;
            set;
        }

        [CanSortItem]
        public string WO_State
        {
            get;
            set;
        }

        public string WO_Reason
        {
            get;
            set;
        }

        [CanSortItem]
        public DateTime Add_Date
        {
            get;
            set;
        }

        [CanSortItem]
        public DateTime? Take_Date
        {
            get;
            set;
        }

        public DateTime? Close_Date
        {
            get;
            set;
        }

        public decimal? Cost
        {
            get;
            set;
        }

        public string Take_Persons
        {
            get => take_Persons;
            set
            {
                take_Persons = value;
                _TakePersonsList = new List<short>();

                if(!string.IsNullOrEmpty(take_Persons))
                {                 
                    foreach(var id in take_Persons.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (short.TryParse(id, out short i))
                        {
                            _TakePersonsList.Add(i);
                        }
                    }                 
                }
            }
        }

        public int? PlanID
        {
            get;
            set;
        }

        public string State_Color
        {
            get;
            set;
        }

        [CanSortItem]
        public DateTime? Mod_Date
        {
            get;
            set;
        }

        public string Mod_Person
        {
            get;
            set;
        }

        public string WO_Level
        {
            get;
            set;
        }

        public int LevelID
        {
            get;
            set;
        }

        public int? Act_Count
        {
            get;
            set;
        }

        public string Dep_Name
        {
            get;
            set;
        }

        public string Assigned_Person
        {
            get;
            set;
        }

        public int File_Count
        {
            get;
            set;
        }

        public int Part_Count
        {
            get;
            set;
        }

        public int Plan_Act_Count
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

        public bool Is_Scheduled_Planned
        {
            get;
            set;
        }

        public List<short> _TakePersonsList
        {
            get;
            set;
        }

        public int CategoryID
        {
            get;
            set;
        }

        public int ReasonID
        {
            get;
            set;
        }

        public int? Ineffective_Count
        {
            get;
            set;
        }

        public DateTime? Person_Take_Date
        {
            get;
            set;
        }

        public int DeviceCategoryID
        {
            get;
            set;
        }

        public GetWOsResponse()
        {
            _TakePersonsList = new List<short>();
            File_Count = 0;
            Part_Count = 0;
        }
    }
}
