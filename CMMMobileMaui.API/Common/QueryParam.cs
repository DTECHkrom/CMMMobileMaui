namespace CMMMobileMaui.API.Common
{
    public class QueryParam
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public override string ToString() =>
            $"{Name}={Value}";
    }
}
