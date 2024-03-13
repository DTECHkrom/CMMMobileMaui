namespace CMMMobileMaui.COMMON
{
    public class DisplayImage
    {
        public string Source
        {
            get;
            init;
        } = string.Empty;

        public Color Color
        {
            get;
            init;
        } = Colors.Black;

        public DisplayImage(string source, Color color)
        {
            Source = source;
            Color = color;
        }   
    }
}
