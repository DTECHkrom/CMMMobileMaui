using System.Threading.Tasks;
using CMMMobileMaui.API.Contracts;
using CMMMobileMaui.API.Interfaces;

namespace CMMMobileMaui.API.Common
{
    public abstract class BaseBLL
    {
        public enum AuthenticationType
        {
            Basic,
            Token
        }

        public IAPIManage apiManage;
        private readonly string root = "api/v1/";

        #region PROPERTY Name

        private string name;

        public string Name
        {
            get
            {
                return root + name;
            }

            set
            {
                name = value;
            }
        }

        #endregion

        #region PROPERTY Authentication

        public AuthenticationType Authentication
        {
            get;
            set;
        } = BaseBLL.AuthenticationType.Token;

        #endregion

        public BaseBLL(IAPIManage apiManage)
        {
            this.apiManage = apiManage;
        }

        #region METHOD GetAsyncResult

        public Task<InvokeReturn<T>> GetAsyncResult<T>(QueryMethod item)
        {
            return apiManage.GetTask<T>($"{Name}/{item}"
                , item.GetJsobStringFromObject(), Authentication);
        }

        #endregion

        #region METHOD PostAsyncResult

        public Task<InvokeReturn<T>> PostAsyncResult<T>(QueryMethod item)
        {
            return apiManage.PostTask<T>($"{Name}/{item}"
                , item.GetJsobStringFromObject(), Authentication);
        }

        #endregion

        #region METHOD PutAsyncResult

        public Task<InvokeReturn<T>> PutAsyncResult<T>(QueryMethod item)
        {
            return apiManage.PutTask<T>($"{Name}/{item}"
                , item.GetJsobStringFromObject(), Authentication);
        }

        #endregion

        #region METHOD DeleteAsyncResult

        public Task<InvokeReturn<T>> DeleteAsyncResult<T>(QueryMethod item)
        {
            return apiManage.DeleteTask<T>($"{Name}/{item}"
                , item.GetJsobStringFromObject(), Authentication);
        }

        #endregion
    }
}
