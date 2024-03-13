using SQLite;

namespace DBMain.Model
{
    public class History
    {
        [PrimaryKey, AutoIncrement]
        public int HistoryID
        {
            get;
            set;
        }

        public int ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public DateTime Mod_Date
        {
            get;
            set;
        }

        public int PersonID
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

    }
}
