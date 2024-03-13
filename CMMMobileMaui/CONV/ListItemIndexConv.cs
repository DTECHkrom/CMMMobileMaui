using CMMMobileMaui.API.Contracts.v1.Responses.Device;
using CMMMobileMaui.API.Contracts.v1.Responses.Part;
using CMMMobileMaui.API.Contracts.v1.Responses.WO;
using System.Collections.ObjectModel;
using System.Globalization;

namespace CMMMobileMaui.CONV
{
    public class ListItemIndexConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && parameter != null)
            {
                var lv = parameter as ListView;

                if (lv != null)
                {
                    if (lv.ItemsSource is List<GetWOsResponse>)
                    {
                        var items = lv.ItemsSource as List<GetWOsResponse>;

                        if (items != null)
                        {
                            return items.IndexOf((GetWOsResponse)value) + 1;
                        }
                    }
                    //else if (lv.ItemsSource is List<vDeviceCMM>)
                    //{
                    //    var items = lv.ItemsSource as List<vDeviceCMM>;

                    //    if (items != null)
                    //    {
                    //        return items.IndexOf((vDeviceCMM)value) + 1;
                    //    }
                    //}
                    else if (lv.ItemsSource is List<DBMain.Model.History>)
                    {
                        var items = lv.ItemsSource as List<DBMain.Model.History>;

                        if (items != null)
                        {
                            return items.IndexOf((DBMain.Model.History)value) + 1;
                        }
                    }
                    else if (lv.ItemsSource is List<GetWOHistResponse>)
                    {
                        var items = lv.ItemsSource as List<GetWOHistResponse>;

                        if (items != null)
                        {
                            return items.IndexOf((GetWOHistResponse)value) + 1;
                        }
                    }
                    else if (lv.ItemsSource is List<GetDeviceStateResponse>)
                    {
                        var items = lv.ItemsSource as List<GetDeviceStateResponse>;

                        if (items != null)
                        {
                            return items.IndexOf((GetDeviceStateResponse)value) + 1;
                        }
                    }
                    //else if (lv.ItemsSource is List<vPartStock>)
                    //{
                    //    var items = lv.ItemsSource as List<vPartStock>;

                    //    if (items != null)
                    //    {
                    //        return items.IndexOf((vPartStock)value) + 1;
                    //    }
                    //}
                    else if (lv.ItemsSource is List<GetPartDetailShortResponse>)
                    {
                        var items = lv.ItemsSource as List<GetPartDetailShortResponse>;

                        if (items != null)
                        {
                            return items.IndexOf((GetPartDetailShortResponse)value) + 1;
                        }
                    }
                    else if (lv.ItemsSource is List<GetPartChangeResponse>)
                    {
                        var items = lv.ItemsSource as List<GetPartChangeResponse>;

                        if (items != null)
                        {
                            return items.IndexOf((GetPartChangeResponse)value) + 1;
                        }
                    }
                    else if (lv.ItemsSource is List<GetPartStocktakingResponse>)
                    {
                        var items = lv.ItemsSource as List<GetPartStocktakingResponse>;

                        if (items != null)
                        {
                            return items.IndexOf((GetPartStocktakingResponse)value) + 1;
                        }
                    }
                    else if (lv.ItemsSource is List<GetStocktakingViewResponse>)
                    {
                        var items = lv.ItemsSource as List<GetStocktakingViewResponse>;

                        if (items != null)
                        {
                            return items.IndexOf((GetStocktakingViewResponse)value) + 1;
                        }
                    }
                    else if (lv.ItemsSource is ObservableCollection<MODEL.WOModel>)
                    {
                        var items = lv.ItemsSource as ObservableCollection<MODEL.WOModel>;

                        if (items != null)
                        {
                            return items.IndexOf((MODEL.WOModel)value) + 1;
                        }
                    }
                }
                else
                {
                    var cv = parameter as CollectionView;

                    if (cv != null)
                    {
                        if (cv.ItemsSource is ObservableCollection<MODEL.WOModel>)
                        {
                            var items = cv.ItemsSource as ObservableCollection<MODEL.WOModel>;

                            if (items != null)
                            {
                                return items.IndexOf((MODEL.WOModel)value) + 1;
                            }
                        }
                        else if (cv.ItemsSource is List<GetStocktakingViewResponse>)
                        {
                            var items = cv.ItemsSource as List<GetStocktakingViewResponse>;

                            if (items != null)
                            {
                                return items.IndexOf((GetStocktakingViewResponse)value) + 1;
                            }
                        }
                    }
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;
        }

    }
}
