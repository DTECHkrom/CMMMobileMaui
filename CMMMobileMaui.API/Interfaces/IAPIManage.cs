using System;
using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts;
using static CMMMobileMaui.API.Common.BaseBLL;

namespace CMMMobileMaui.API.Interfaces
{
    public interface IAPIManage
    {
        event EventHandler OnError;
        event EventHandler OnSuccess;

        #region PROPERTY Host

        string Host
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY Login

        string Login
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY Pass

        string Pass
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY IsAnonymous

        bool IsAnonymous
        {
            get;
            set;
        }

        #endregion

        #region PROPERTY IsSSL

        bool IsSSL
        {
            get;
            set;
        }

        #endregion
        Task<InvokeReturn<T>> GetTask<T>(string path, string obj = null, AuthenticationType authentication = AuthenticationType.Token );

        Task<InvokeReturn<T>> PostTask<T>(string path, string obj, AuthenticationType authentication);

        Task<InvokeReturn<T>> PutTask<T>(string path, string obj, AuthenticationType authentication);

        Task<InvokeReturn<T>> DeleteTask<T>(string path, string obj, AuthenticationType authentication);

        Uri GetHostUri();

        string GetAuthorizationHeaderText();

        bool IsDataSet();
    }
}
