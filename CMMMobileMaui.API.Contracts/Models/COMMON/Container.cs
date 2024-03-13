namespace CMMMobileMaui.API.Contracts.Models.COMMON
{
    public class Container
    {
        internal static ITextDictionaryResource TextDictionaryResource;

        public static void SetDictionaryResource(ITextDictionaryResource textDictionaryResource) =>
            TextDictionaryResource = textDictionaryResource;
    }
}
