using CommunityToolkit.Maui.Animations;
using CommunityToolkit.Maui.Behaviors;

namespace CMMMobileMaui.CUST
{
    public class LabelIconButton : Label
    {
        #region PROPERTY

        public event EventHandler Clicked;

        #endregion

        #region PROPERTY CommandProperty

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(System.Windows.Input.ICommand), typeof(LabelIconButton), default(System.Windows.Input.ICommand), propertyChanged: OnCommandPropertyChanged);

        private static void OnCommandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is LabelIconButton lbl)
            {
                var animBehavior = lbl.Behaviors.OfType<AnimationBehavior>().FirstOrDefault();

                if (animBehavior != null)
                {
                    animBehavior.Command = (System.Windows.Input.ICommand)newValue;
                }
            }
        }

        public System.Windows.Input.ICommand Command
        {
            get => (System.Windows.Input.ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        #endregion

        #region PROPERTY CommandParameterProperty

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(LabelIconButton), default(object), propertyChanged: OnCommandParameterPropertyChanged);

        private static void OnCommandParameterPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is LabelIconButton lbl)
            {
                var animBehavior = lbl.Behaviors.OfType<AnimationBehavior>().FirstOrDefault();

                if (animBehavior != null)
                {
                    animBehavior.CommandParameter = newValue;
                }
            }
        }
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        #endregion

        public LabelIconButton()
        {
            this.FontSize = 32;
            this.FontFamily = "MaterialIconsRegular";
            this.HorizontalTextAlignment = TextAlignment.Center;
            this.VerticalTextAlignment = TextAlignment.Center;
            this.TextColor = Colors.Black;


            AnimationBehavior animationBehavior = new();
            var fadeAnimation = new FadeAnimation();
            fadeAnimation.Opacity = 0.5;
            animationBehavior.AnimationType = fadeAnimation;

            this.Behaviors.Add(animationBehavior);

            TapGestureRecognizer tapGestureRecognizer = new();
            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;

            this.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private void TapGestureRecognizer_Tapped(object? sender, TappedEventArgs e)
        {
            if (Clicked != null)
            {
                Clicked?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
