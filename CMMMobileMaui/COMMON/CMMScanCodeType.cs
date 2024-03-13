namespace CMMMobileMaui.COMMON
{
    public interface ICMMCodeType
    {
        string StartCodePattern
        {
            get;
        }
        int? GetScannedID();

        bool IsMatch();
    }

    public class CodeTypeManager
    {
        public int? GetScannedID(string code)
        {
            int? id;

            if ((id = GetScannedID(new CodeTypePartLocation(code))) != null)
            {
                return id;
            }

            return null;
        }

        private int? GetScannedID(ICMMCodeType codeType) =>       
            codeType.IsMatch() ? codeType.GetScannedID(): null;
    }

    public class CodeTypePartLocation : ICMMCodeType
    {
        public string StartCodePattern => "pl:";
        private string code;
        public CodeTypePartLocation(string code)
        {
            this.code = code;
        }
        public int? GetScannedID()
        {
            if (int.TryParse(code.Replace(StartCodePattern, ""), out int id))
            {
                return id;
            }

            return null;
        }

        public bool IsMatch() =>
            code.StartsWith(StartCodePattern);
    }
}
