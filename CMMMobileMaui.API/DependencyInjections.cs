using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;

namespace CMMMobileMaui.API
{
    public static class DependencyInjections
    {
        public static void AddServices(ServiceCollection services)
        {
            services.AddSingleton<Interfaces.IAPIManage, APIManage>();
            services.AddSingleton<Interfaces.IDeviceController, Controllers.v1.DeviceController>();
            services.AddSingleton<Interfaces.ISetController, Controllers.v1.SetController>();
            services.AddSingleton<Interfaces.IOtherController, Controllers.v1.OtherController>();
            services.AddSingleton<Interfaces.IWOController, Controllers.v1.WOController>();
            services.AddSingleton<Interfaces.IActController, Controllers.v1.ActController>();
            services.AddSingleton<Interfaces.IActPerController, Controllers.v1.ActPerController>();
            services.AddSingleton<Interfaces.IPlanController, Controllers.v1.PlanController>();
            services.AddSingleton<Interfaces.IPartController, Controllers.v1.PartController>();
            services.AddSingleton<Interfaces.IIdentityController, Controllers.IdentityController>();
            services.AddSingleton<Interfaces.IUserController, Controllers.v1.UserController>();

            services.AddHttpClient("CMM", (service, client) =>
            {
                var apiMan = service.GetService<Interfaces.IAPIManage>();

                if (apiMan != null
                && apiMan.IsDataSet())
                {
                    client.BaseAddress = apiMan.GetHostUri();
                    client.Timeout = new TimeSpan(0, 0, 10);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                }
            });
        }
    }
}
