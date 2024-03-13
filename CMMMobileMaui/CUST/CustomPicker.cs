namespace CMMMobileMaui.CUST
{
    public class CustomPicker2 : Picker, COMMON.IRequired
    {
        #region PROPERTY IsRequiredProperty

        public static BindableProperty IsRequiredProperty = BindableProperty.Create(nameof(IsRequired), typeof(bool), typeof(CustomPicker2), default(bool));

        public bool IsRequired
        {
            get
            {
                return (bool)GetValue(IsRequiredProperty);
            }

            set
            {
                SetValue(IsRequiredProperty, value);
            }
        }

        #endregion

        #region Cstr

        public CustomPicker2()
        {
            this.BackgroundColor = Colors.White;
        }

        #endregion
    }
}
