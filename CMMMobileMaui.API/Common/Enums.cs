namespace CMMMobileMaui.API.Common
{
    internal static class Enums
    {
        public enum PartDictListType : int
        {
            warehouse = 1,
            producent = 2,
            unit = 3,
            currency = 4
        }

        public enum DeviceDictListType : int
        {
            category = 1,
            supplier = 2,
        }

        public enum WorkOrderDictListType : int
        {
            category = 1,
            level = 2,
            reason = 3,
            state = 4,
            department = 5
        }

        public enum ActivityDictListType : int
        {
            category = 1
        }
    }
}
