using Microsoft.Maui.Graphics;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.CUST
{
    //public class CustomAutoCompleteView2 : AutoCompleteView, COMMON.IRequired
    //{
    //    private string _suggestedText = string.Empty;

    //    #region PROPERTY IsRequiredProperty

    //    public static BindableProperty IsRequiredProperty = BindableProperty.Create(nameof(IsRequired), typeof(bool), typeof(CustomAutoCompleteView2), default(bool));

    //    public bool IsRequired
    //    {
    //        get
    //        {
    //            return (bool)GetValue(IsRequiredProperty);
    //        }

    //        set
    //        {
    //            SetValue(IsRequiredProperty, value);
    //        }
    //    }

    //    #endregion

    //    #region Cstr

    //    public CustomAutoCompleteView2()
    //    {
    //        this.BackgroundColor = Colors.White;
    //        this.NoResultsMessage = CONV.TranslateExtension.GetResourceText("no_result_found");
    //        this.BorderColor = Colors.Black;
    //        this.BorderThickness = new Thickness(2);
    //        this.Margin = new Thickness(4, 2, 4, 2);
    //        this.SuggestionViewHeight = 200;
    //        this.FontSize = 14;
    //        this.SuggestionViewBackgroundColor = Colors.White;
    //        this.TextChanged += CustomAutoCompleteView_TextChanged;
    //        this.SuggestionItemSelected += CustomAutoCompleteView_SuggestionItemSelected;
    //        this.Focused += CustomAutoCompleteView_Focused;
    //    }

    //    #region EVENT CustomAutoCompleteView_Focused

    //    private void CustomAutoCompleteView_Focused(object sender, FocusEventArgs e)
    //    {
    //        if (string.IsNullOrWhiteSpace(this.Text))
    //        {
    //            this.ShowSuggestions();
    //        }
    //        else
    //        {
    //            if(this.FilteredItems != null)
    //            {                
    //                while(this.FilteredItems.GetEnumerator().MoveNext())
    //                {
    //                    this.HideSuggestions();

    //                    break;
    //                }
    //            }
    //        }
    //    }

    //    #endregion

    //    #region EVENT CustomAutoCompleteView_SuggestionItemSelected

    //    private void CustomAutoCompleteView_SuggestionItemSelected(object sender, Telerik.XamarinForms.Input.AutoComplete.SuggestionItemSelectedEventArgs e)
    //    {
    //        BorderColor = Colors.Black;

    //        if(e.DataItem == null)
    //        {
    //            BorderColor = Colors.Red;
    //        }
    //    }

    //    #endregion

    //    #region EVENT CustomAutoCompleteView_TextChanged

    //    private void CustomAutoCompleteView_TextChanged(object sender, TextChangedEventArgs e)
    //    {
    //        if (IsRequired)
    //        {
    //            if (!string.IsNullOrEmpty(e.NewTextValue) 
    //                && this.FilteredItems != null)
    //            {
    //                BorderColor = Colors.Black;
    //                return;
    //            }

    //            BorderColor = Colors.Red;
    //        }

    //        if(e.OldTextValue == _suggestedText 
    //            && string.IsNullOrWhiteSpace(e.NewTextValue))
    //        {
    //            this.ShowSuggestions();
    //        }

    //        _suggestedText = e.NewTextValue;
    //    }

    //    #endregion

    //    #endregion

    //    #region METHOD InitData

    //    public void InitData(string text)
    //    {
    //        this.Text = text;
    //        BorderColor = Colors.Black;
    //    }

    //    #endregion

    //    #region METHOD InitValues

    //    public void InitValues()
    //    {
    //        if(IsRequired)
    //        {
    //            BorderColor = Colors.Red;
    //        }
    //    }

    //    #endregion
    //}
}
