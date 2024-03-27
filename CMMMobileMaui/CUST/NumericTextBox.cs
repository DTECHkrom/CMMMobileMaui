using System.Globalization;

namespace CMMMobileMaui.CUST
{
    public class NumericTextBox : CustomEntry
    {
        #region Bindables

        public static readonly BindableProperty NumericValueProperty = BindableProperty.Create(
            "NumericValue",
            typeof(decimal?),
            typeof(NumericTextBox),
            null,
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
              //  ((NumericTextBox)bindable).Text = newValue == null ? string.Empty : newValue.ToString();

                if(bindable is NumericTextBox textBox)
                {
                    var value = (decimal?)newValue;

                    textBox.SetDisplayFormat(value);
                    
                }
            }
        );

        public static readonly BindableProperty NumericValueFormatProperty = BindableProperty.Create(
            "NumericValueFormat",
            typeof(string),
            typeof(NumericTextBox),
            "N0",
            BindingMode.TwoWay,
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                //SetDisplayFormat((NumericTextBox)bindable);
            }
        );

        #endregion

        #region Constructor

        public NumericTextBox()
        {
            Keyboard = Keyboard.Numeric;
            //  Focused += OnFocused;
            // Unfocused += OnUnfocused;
            this.BackgroundColor = Colors.White;
            // this.TextChanged += NumericTextBox_TextChanged;
        }

        private string tempOldValue = string.Empty;

        private void NumericTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //  var newLength = e.NewTextValue.Length;
            //  var oldLength = (string.IsNullOrEmpty(e.OldTextValue)) ? e.NewTextValue.Length : e.OldTextValue.Length;
        }

        #endregion

        #region Events

        private void OnFocused(object sender, FocusEventArgs e)
        {
            // SetDisplayFormat(this);
        }

        public void SetDisplayFormat()
        {
            if (string.IsNullOrEmpty(Text))
                return;

            var numberFormant = CultureInfo.CurrentCulture.NumberFormat;
            var _text = Text.Replace(numberFormant.NumberGroupSeparator, string.Empty).Replace(numberFormant.NumberDecimalSeparator, ".");

            if (decimal.TryParse(_text, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal numericValue))
            {
                if (NumericValueFormat.StartsWith("N"))
                {
                    int round = Convert.ToInt32(NumericValueFormat.Substring(1));
                    NumericValue = Math.Round(numericValue, round);
                }
                else
                {
                    NumericValue = numericValue;
                }
            }
            else
            {
                NumericValue = null;
            }

            // SetDisplayFormat(this);
        }

        private void OnUnfocused(object sender, FocusEventArgs e)
        {

        }

        #endregion

        #region Properties

        public decimal? NumericValue
        {
            get { return (decimal?)GetValue(NumericValueProperty); }
            set { SetValue(NumericValueProperty, value); }
        }

        public string NumericValueFormat
        {
            get { return (string)GetValue(NumericValueFormatProperty) ?? "N0"; }
            set
            {
                var _value = string.IsNullOrWhiteSpace(value) ? "N0" : value;
                SetValue(NumericValueFormatProperty, value);
            }
        }
        #endregion

        #region Methods

        private void SetDisplayFormat(decimal? value)
        {
            if (this.NumericValue.HasValue)
            {
                var form = GetDisplaynumericFormat();
                this.Text = NumericValue.Value.ToString(form);
            }
            else
            {
                Text = string.Empty;
            }
        }
        private bool IsNumericFormatWithN() =>
            this.NumericValueFormat.StartsWith("N");

        private int GetDecimalPlaces() =>
            Convert.ToInt32(NumericValueFormat.Substring(1));

        private string GetDisplaynumericFormat() =>
            !IsNumericFormatWithN() ? string.Empty
            : GetDecimalPlaces() == 0 ? string.Empty
            : $"0.{new string('#', GetDecimalPlaces())}";

        #endregion
    }
}
