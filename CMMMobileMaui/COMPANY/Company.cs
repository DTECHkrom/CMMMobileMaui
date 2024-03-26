using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMMMobileMaui.API.Interfaces;
using CMMMobileMaui.COMMON;
using CMMMobileMaui.COMPANY.ExtraContent;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.COMPANY
{
    public abstract class Company
    {
        public IUserController personBLL;
        public IIdentityController identityController;

        private List<CompanyExtraContent> companyExtraContentList;
        public Company()
        {
            personBLL = (IUserController)API.MainObjects.Instance.ServiceProvider!.GetRequiredService(typeof(IUserController));
            identityController = (IIdentityController)API.MainObjects.Instance.ServiceProvider!.GetRequiredService(typeof(IIdentityController));
            companyExtraContentList = new List<CompanyExtraContent>();
        }

        public void ClearExtraContent() =>
            companyExtraContentList.Clear();

        public void SetTitleForExtraContent() =>
            companyExtraContentList.ForEach(content => content.SetTitle());

        public void InitExtraContent()
        {
            InitContentToShell();
        }

        public async ValueTask<bool> IsExtraContentAfterLogin()
        {
            var startPage =  companyExtraContentList.FirstOrDefault(tt => tt.GetType().BaseType == typeof(AfterLoginExtraContent));

            if (startPage == null)
                return false;

            var afterLoginPage = (AfterLoginExtraContent)startPage;

            return await afterLoginPage.ShouldOpenOnStart();
        }

        private void InitContentToShell()
        {
            companyExtraContentList.ForEach(companyExtraContent =>
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    Shell.Current.Items.Add(companyExtraContent.GetShellContent());
                });
            });
        }

        protected void AddExtraContent(CompanyExtraContent companyExtraContent)
        {
            companyExtraContentList.Add(companyExtraContent);       
        }

        public virtual async Task<bool> SaveUserLogin(string code)
        {
            if (code.StartsWith("u:"))
            {
                var updateResponse = await personBLL.UpdateCode(new API.Contracts.v1.Requests.User.UpdateUserCodeRequest
                {
                    Code = code,
                    PersonID = API.MainObjects.Instance.CurrentUser!.PersonID
                });

                if(updateResponse.IsValid
                    && updateResponse.Data)
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        public virtual async Task<bool> ScanUserLogin(string code)
        {
            if (code.StartsWith("u:"))
            {


                var serialNumber = DependencyService.Get<ISerialNumberService>()
                    .GetSerialNumber();

                var personResponse = await identityController.LoginCode(new API.Contracts.v1.Requests.Identity.LoginCodeRequest
                {
                    Code = code
                    , Mobile_Info = serialNumber
                });

                if (personResponse.IsValid)
                {
                    API.MainObjects.Instance.CurrentUser = personResponse.Data;

                    return true;
                }              
            }

            return false;
        }
    }
}
