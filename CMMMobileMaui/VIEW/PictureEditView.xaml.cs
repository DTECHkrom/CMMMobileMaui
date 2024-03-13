using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.COMMON.Effects;
using CMMMobileMaui.CUST;
using CommunityToolkit.Maui.Behaviors;
using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace CMMMobileMaui.VIEW
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PictureEditView : ContentPage
    {
        #region FIELDS

        private WOFile fileItem;
        Dictionary<long, (SKPath, SKPaint)> inProgressPaths = new Dictionary<long, (SKPath, SKPaint)>();
        List<(SKPath, SKPaint)> completedPaths = new List<(SKPath, SKPaint)>();

        public event EventHandler<WOFile> OnFileSave;

        SKPaint paint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Red,
            StrokeWidth = 10,
            StrokeCap = SKStrokeCap.Round,
            StrokeJoin = SKStrokeJoin.Round
        };

        SKBitmap saveBitmap;
        SKBitmap orgBitmap;

        #endregion

        #region Cstr
        public PictureEditView(WOFile fileItem)
        {
            InitializeComponent();
            this.fileItem = fileItem;
            btnSelectColor.TextColor = Colors.Red;
            SetCanSaveEndUndo();

            orgBitmap = SKBitmap.Decode(this.fileItem.Data);
            saveBitmap = orgBitmap.Copy();

            canvasView.InvalidateSurface();
        }

        #endregion

        #region EVENT Button_Clicked

        private void Button_Clicked(object sender, EventArgs e)
        {
            var item = completedPaths.LastOrDefault();

            if (item != default)
            {
                completedPaths.Remove(item);

                saveBitmap = orgBitmap.Copy();

                UpdateBitmap();
            }
        }

        #endregion

        #region EVENT Button_Clicked_1

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            using (SKImage image = SKImage.FromBitmap(saveBitmap))
            {
                SKData data = image.Encode(SKEncodedImageFormat.Jpeg, 100);

                WOFile newFile = new WOFile();

                newFile.WorkOrderDataID = fileItem.WorkOrderDataID;
                newFile.Extension = fileItem.Extension;
                newFile.File_Name = fileItem.File_Name;
                newFile.WorkOrderID = fileItem.WorkOrderID;
                newFile.Data = data.ToArray();

                await Shell.Current.Navigation.PopModalAsync();

                OnFileSave?.Invoke(this, newFile);
            }
        }

        #endregion

        #region EVENT canvasView_PaintSurface

        private void canvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            //else if (saveBitmap.Width < info.Width || saveBitmap.Height < info.Height)
            //{
            //    SKBitmap newBitmap = new SKBitmap(Math.Max(saveBitmap.Width, info.Width),
            //                                      Math.Max(saveBitmap.Height, info.Height));

            //    using (SKCanvas newCanvas = new SKCanvas(newBitmap))
            //    {
            //        newCanvas.Clear();
            //        newCanvas.DrawBitmap(saveBitmap, 0, 0);
            //    }

            //    saveBitmap = newBitmap;
            //}

            canvas.Clear();

            canvas.DrawBitmap(saveBitmap, new SKRect(0, 0, saveBitmap.Width, saveBitmap.Height), info.Rect, BitmapStretch.Fill);
        }

        #endregion

        #region METHOD SetCanSaveEndUndo

        private void SetCanSaveEndUndo()
        {
            btnCancel.IsEnabled = btnSave.IsEnabled = completedPaths.Count > 0;
        }

        #endregion

        #region EVENT OnTouchEffectAction

        void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {
            if (slPalette.IsVisible)
                return;

            switch (args.Type)
            {
                case TouchActionType.Pressed:

                    if (!inProgressPaths.ContainsKey(args.Id))
                    {
                        SKPath path = new SKPath();
                        path.MoveTo(ConvertToPixel(args.Location));
                        inProgressPaths.Add(args.Id, (path, paint));
                        UpdateBitmap();
                    }
                    break;

                case TouchActionType.Moved:
                    if (inProgressPaths.ContainsKey(args.Id))
                    {
                        SKPath path = inProgressPaths[args.Id].Item1;
                        path.LineTo(ConvertToPixel(args.Location));
                        UpdateBitmap();
                    }
                    break;

                case TouchActionType.Released:

                    if (inProgressPaths.ContainsKey(args.Id))
                    {
                        completedPaths.Add(inProgressPaths[args.Id]);
                        inProgressPaths.Remove(args.Id);
                        UpdateBitmap();
                    }
                    break;

                case TouchActionType.Cancelled:
                    if (inProgressPaths.ContainsKey(args.Id))
                    {
                        inProgressPaths.Remove(args.Id);
                        UpdateBitmap();
                    }
                    break;
            }
        }

        #endregion

        #region EVENT ConvertToPixel

        SKPoint ConvertToPixel(Point pt)
        {
            return new SKPoint((float)(saveBitmap.Width * pt.X / canvasView.Width),
                               (float)(saveBitmap.Height * pt.Y / canvasView.Height));
        }

        #endregion

        #region EVENT UpdateBitmap

        void UpdateBitmap()
        {
            using (SKCanvas saveBitmapCanvas = new SKCanvas(saveBitmap))
            {
                // saveBitmapCanvas.Clear();
                foreach (var path in completedPaths)
                {
                    saveBitmapCanvas.DrawPath(path.Item1, path.Item2);
                }

                foreach (var path in inProgressPaths.Values)
                {
                    saveBitmapCanvas.DrawPath(path.Item1, path.Item2);
                }
            }

            canvasView.InvalidateSurface();

            SetCanSaveEndUndo();
        }

        #endregion

        #region METHOD SetPaintColor

        private void SetPaintColor(SKColor? newColor, float? newStroke = null)
        {
            var stroke = paint.StrokeWidth;
            var color = paint.Color;

            if (newStroke.HasValue)
            {
                stroke = newStroke.Value;
            }

            if (newColor.HasValue)
            {
                color = newColor.Value;
            }

            paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = color,
                StrokeWidth = stroke,
                StrokeCap = SKStrokeCap.Round,
                StrokeJoin = SKStrokeJoin.Round
            };
        }

        #endregion

        
        #region EVENT rect_Clicked

        private void rect_Clicked(object sender, EventArgs e)
        {
            var con = sender as LabelIconButton;

            if (con != null)
            {
                SetPaintColor(SKColor.Parse(con.TextColor.ToHex()));
                btnSelectColor.TextColor = con.TextColor;               
                slPalette.IsVisible = false;
            }
        }

        #endregion

        #region EVENT Slider_ValueChanged

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            SetPaintColor(null, 50 * (float)e.NewValue);
        }

        #endregion

        private void btnSelectColor_Clicked(object sender, EventArgs e)
        {
            slPalette.IsVisible = !slPalette.IsVisible;
        }
    }
}