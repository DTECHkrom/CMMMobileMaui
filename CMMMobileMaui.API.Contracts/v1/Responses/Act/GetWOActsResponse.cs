using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CMMMobileMaui.API.Contracts.v1.Responses.Act
{
    public class GetWOActsResponse: INotifyPropertyChanged
    {
        private string act_Persons;
        private bool isAddPerson;

        public int WorkOrderID
        {
            get;
            set;
        }

        public int ActivityID
        {
            get;
            set;
        }

        public decimal? Work_Load
        {
            get;
            set;
        }

        public string Act_Category
        {
            get;
            set;
        }
        public int? Workers
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Act_Persons
        {
            get => act_Persons;
            set
            {
                act_Persons = value;

                if (!string.IsNullOrEmpty(act_Persons))
                {
                    _ActPersonList = new List<short>();

                    foreach (var id in act_Persons.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (short.TryParse(id, out short i))
                        {
                            _ActPersonList.Add(i);
                        }
                    }
                }
            }
        }

        public DateTime Add_Date
        {
            get;
            set;
        }

        public bool _IsAddPerson
        {
            get => isAddPerson;
            set
            {
                isAddPerson = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_IsAddPerson)));
            }
        }

        private decimal workLoad;

        public decimal _WorkLoad
        {
            get => workLoad;
            set
            {
                workLoad = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(_WorkLoad)));
            }
        }

        public List<short> _ActPersonList
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
