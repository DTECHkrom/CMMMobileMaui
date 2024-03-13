using CMMMobileMaui.API.Contracts.v1.Responses.Other;
using CMMMobileMaui.COMPANY.CEAZAR_;
using CMMMobileMaui.COMPANY.PUK_;

namespace CMMMobileMaui.COMPANY
{
    public class CompanyFactory
    {
        public static Company GetAppCompany(GetAppSettResponse data)
        {
            if (data != null)
            {
                if (!string.IsNullOrEmpty(data.CompanyData))
                {
                    string compName = data.CompanyData.ToLower();

                    switch (compName)
                    {
                        case "bianor":

                            return new ROSTI();

                        case "puk":

                            return new PUK();

                        case "cezar":

                            return new Cezar();
                    }
                }
            }

            return new BaseCompany();
        }
    }
}
