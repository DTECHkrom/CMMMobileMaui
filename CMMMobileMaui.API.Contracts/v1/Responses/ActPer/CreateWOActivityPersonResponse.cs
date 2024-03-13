using System;

namespace CMMMobileMaui.API.Contracts.v1.Responses.ActPer
{
    public class CreateWOActivityPersonResponse
    {
        public int ActivityPersonID
        {
            get;
            set;
        }

        public int ActivityID
        {
            get;
            set;
        }

        public short PersonID
        {
            get;
            set;
        }

        public decimal Work_Load
        {
            get;
            set;
        }

        public DateTime Add_Date
        {
            get;
            set;
        }
    }
}
