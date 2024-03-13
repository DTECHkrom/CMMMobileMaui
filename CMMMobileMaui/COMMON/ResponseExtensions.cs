using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.COMMON;
using System.Reflection;

namespace CMMMobileMaui
{
    internal static class ResponseExtensions
    {
        public static bool IsResponseWithData<T>(this InvokeReturn<T> invokeReturn) =>
            invokeReturn != null
            && invokeReturn.Data != null
            && IsDataListWithItems(invokeReturn)
            && invokeReturn.IsValid;

        public static bool IsResponseWithData<T>(this InvokeReturn<T> invokeReturn, IAlertPopup alertPopup)
        {
            if (IsResponseWithData(invokeReturn))
            {
                return true;
            }
            else
            {
                if (invokeReturn.IsErrorMsg())
                {
                    alertPopup.OpenAlertMessage(invokeReturn.GetErrorMsg());
                }
            }

            return false;
        }

        private static bool IsDataListWithItems<T>(InvokeReturn<T> invokeReturn)
        {
            var type = invokeReturn.Data!.GetType();

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                if(invokeReturn.Data is System.Collections.IList list)
                {
                    if (list.Count > 0)
                    {
                        return true;
                    }
                }

                return false;
            }

            return true;
        }

    }
}
