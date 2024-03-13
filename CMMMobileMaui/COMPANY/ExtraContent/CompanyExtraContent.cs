using System;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.COMPANY.ExtraContent
{
    public abstract class CompanyExtraContent
    {
        private readonly string textKey;
        private readonly ShellContent content;

        public CompanyExtraContent(string textKey
            , string icon
            , Type contentTemplateType)
        {
            this.textKey = textKey;
            this.content = new ShellContent()
            {
                Title = CONV.TranslateExtension.GetResourceText(textKey),
                Icon = icon,
                ContentTemplate = new DataTemplate(contentTemplateType)               
            };
        }

        public CompanyExtraContent(string textKey
            , string icon
            , string routePath
            , Type contentTemplateType)
        {
            this.textKey = textKey;
            this.content = new ShellContent()
            {
                Title = CONV.TranslateExtension.GetResourceText(textKey),
                Icon = icon,
                Route = routePath,
                ContentTemplate = new DataTemplate(contentTemplateType)
            };
        }

        public ShellContent GetShellContent() =>
            content;

        public void SetTitle() =>
            content.Title = CONV.TranslateExtension.GetResourceText(textKey);
    }
}
