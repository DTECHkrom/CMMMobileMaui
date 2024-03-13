using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using CMMMobileMaui.COMMON;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using System.ComponentModel;
using Paint = Android.Graphics.Paint;
using Resource = Microsoft.Maui.Controls.Compatibility.Resource;
using ShapeDrawable = Android.Graphics.Drawables.ShapeDrawable;


namespace XAMProjEmpty.Droid.CustRenderer
{
    //public class CustomEntryRenderer : EntryRenderer
    //{
    //    private IRequired _customEntry;
    //    private EditText _nativeEditText;

    //    public CustomEntryRenderer(Context context) : base(context)
    //    {
    //    }

    //    public CustomEntryRenderer() : base(null)
    //    {
    //        throw new Exception($"Should not be used in [{nameof(CustomEntryRenderer)}]");
    //    }

    //    protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
    //    {
    //        base.OnElementChanged(e);

    //        if (e.NewElement != null && e.NewElement is IRequired)
    //        {
    //            _customEntry = ((IRequired)e.NewElement);
    //            //   Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Blue);

    //            _nativeEditText = (EditText)Control;

    //            if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
    //            {
    //                Control.SetTextCursorDrawable(Resource.Drawable.my_cursor); //This API Intrduced in android 10
    //            }
    //            else
    //            {
    //                IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
    //                IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");
    //                // my_cursor is the xml file name which we defined above
    //                JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Drawable.my_cursor);
    //            }

    //            if (_customEntry.IsRequired)
    //            {
    //                if (string.IsNullOrEmpty(Element.Text))
    //                {
    //                    _nativeEditText.Background = GetShapeForControl(Android.Graphics.Color.Red);
    //                }
    //                else
    //                {
    //                    _nativeEditText.Background = GetShapeForControl(Android.Graphics.Color.Black);
    //                }
    //            }
    //            else
    //            {
    //                _nativeEditText.Background = GetShapeForControl(Android.Graphics.Color.Black);
    //            }

    //            Control.SetPadding(10, 10, 10, 10);
    //        }
    //    }

    //    protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
    //    {
    //        base.OnElementPropertyChanged(sender, e);

    //        if (_customEntry != null && _customEntry.IsRequired)
    //        {
    //            if (e.PropertyName == "Text")
    //            {
    //                if (string.IsNullOrEmpty(Element.Text))
    //                {
    //                    _nativeEditText.Background = GetShapeForControl(Android.Graphics.Color.Red);
    //                }
    //                else
    //                {
    //                    _nativeEditText.Background = GetShapeForControl(Android.Graphics.Color.Black);
    //                }
    //            }
    //        }
    //    }

    //    private ShapeDrawable GetShapeForControl(Android.Graphics.Color color)
    //    {
    //        var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RectShape());
    //        shape.Paint.Color = color;
    //        // Console.WriteLine(Android.App.Application.Context.Resources.DisplayMetrics.Density);
    //        shape.Paint.UnderlineText = false;
    //        shape.Paint.SetStyle(Paint.Style.Stroke);
    //        shape.Paint.StrokeWidth = 4f * Android.App.Application.Context.Resources.DisplayMetrics.Density;

    //        return shape;
    //    }
    //}
}