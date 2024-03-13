using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMMMobileMaui.API.Contracts.v1.Responses.WO
{
    public class CreateWOResponse
    {
        public int WorkOrderID
        {
            get;
            set;
        }

        public short AddPersonID
        {
            get;
            set;
        }

        public DateTime AddDate
        {
            get;
            set;
        }

        public int StateID
        {
            get;
            set;
        }

        public int LevelID
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
    }
}
