using SQLite;

namespace DBMain
{
    public class Engine
    {
        #region PROPERTY MainCon

        public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

        private readonly string databaseName = "CMMHist.db3";
        private SQLiteAsyncConnection? mainCon;

        #endregion

        #region Cstr

        public Engine()
        {
        }

        #endregion

        #region METHOD Init

        async Task Init()
        {
            if (mainCon is not null)
                return;

            mainCon = new SQLiteAsyncConnection(GetDatabasePath(), Flags);
            
            var createTable = await mainCon.CreateTableAsync<Model.History>();
        }

        #endregion

        #region METHOD GetDatabasePath

        private string GetDatabasePath() => 
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), databaseName);


        #endregion

        #region METHOD InsertHist

        public async Task<int> InsertHist(Model.History hist)
        {
            await Init();

            string queryString = $"SELECT * FROM History WHERE Type = '{hist.Type}' AND Name = '{hist.Name}' AND PersonID={hist.PersonID}";

            List<Model.History> items = mainCon!.QueryAsync<Model.History>(queryString).Result;

            if (items != null && items.Count > 0)
            {
                return -1;
            }

            return await mainCon!.InsertAsync(hist, typeof(Model.History));
        }

        #endregion

        #region METHOD UpdateHist

        public async Task<int> UpdateHist(Model.History hist)
        {
            await Init();
            return await mainCon!.UpdateAsync(hist);
        }

        #endregion

        #region METHOD GetAllHistory

        public async Task<List<Model.History>> GetAllHistory(int personID)
        {
            await Init();

            return await mainCon!.Table<Model.History>()
                .Where(tt => tt.PersonID == personID && tt.Type == "d")
                .OrderByDescending(tt => tt.Mod_Date)
                .Take(50)
                .ToListAsync();
        }

        #endregion

        #region METHOD GetAllFilesHistory

        public async Task<List<Model.History>> GetAllFilesHistory(int personID)
        {
            await Init();

            return await mainCon!.Table<Model.History>()
                .Where(tt => tt.PersonID == personID && tt.Type == "f")
                .OrderByDescending(tt => tt.Mod_Date)
                .Take(50)
                .ToListAsync();
        }

        #endregion

        #region METHOD GetSingleFileHistory

        public async Task<Model.History> GetSingleFileHistory(int personID, string name)
        {
            await Init();

            return await mainCon!.Table<Model.History>()
                .Where(tt => tt.PersonID == personID && tt.Type == "f" && tt.Name == name)
                .FirstOrDefaultAsync();
        }

        #endregion

        #region METHOD DeleteHistory

        public async Task<int> DeleteHistory(int personID)
        {
            await Init();

            return await mainCon!.ExecuteScalarAsync<int>(string.Format("DELETE FROM History WHERE PersonID={0}", personID));
        }

        #endregion

        #region METHOD DeleteHistoryAll

        public async Task<int> DeleteHistoryAll()
        {
            await Init();

            return await mainCon!.ExecuteScalarAsync<int>(string.Format("DELETE FROM History"));
        }

        #endregion

        #region METHOD DeletePartInv

        public async Task<int> DeletePartInv(int personID)
        {
            await Init();

            return await mainCon!.ExecuteScalarAsync<int>(string.Format("DELETE FROM PartInv WHERE PersonID={0}", personID));
        }

        #endregion

        #region METHOD DeletePartInvAll

        public async Task<int> DeletePartInvAll()
        {
            await Init();

            return await mainCon!.ExecuteScalarAsync<int>(string.Format("DELETE FROM PartInv"));
        }

        #endregion

        #region METHOD CloseConnection

        public async void CloseConnection()
        {
            if (mainCon != null)
            {
                await mainCon.CloseAsync();
                mainCon = null;
            }
        }

        #endregion
    }
}
