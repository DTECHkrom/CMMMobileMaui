namespace CMMMobileMaui.COMMON
{
    public static class ServiceProviderInitializer
    {
        public static void Init()
        {
            ServiceCollection services = new ServiceCollection();

            //if(scannerService is not null)
            //{
            //    services.AddTransient((pp) => scannerService);
            //}
            services.AddSingleton<VM.VMMainShell>();
            services.AddTransient<VM.VMWorkOrderListAll>();
            services.AddTransient<VM.VMMainView>();
            services.AddTransient<VM.VMDeviceSearch>();
            services.AddTransient<VM.VMScan>();
            services.AddTransient<VM.VMNewDevice>();
            services.AddTransient<VM.VMNewPart>();
            services.AddTransient<VM.VMPartStocktaking>();
            services.AddTransient<VM.VMPartGiver2>();
            services.AddTransient<VM.VMWorkOrderPartMain>();
            services.AddTransient<VM.VMBaseList>();
            services.AddTransient<VM.VMDeviceDetail>();
            services.AddTransient<VM.VMDeviceDocumentation>();
            services.AddTransient<VM.VMDeviceSet>();
            services.AddTransient<VM.VMDevice>();
            services.AddTransient<VM.VMLogin>();
            services.AddTransient<VM.VMPartDetail>();
            services.AddTransient<VM.VMPartInvenMain>();
            services.AddTransient<VM.VMTestPage>();
            services.AddTransient<VM.VMUpadeApp>();
            services.AddTransient<VM.VMWorkOrderFileMain>();
            services.AddTransient<VM.VMWorkOrderHistory>();
            services.AddTransient<VM.VMWorkOrderUserScheduler>();
            services.AddTransient<VM.VMWorkOrderSingle>();
            services.AddTransient<VM.VMPictureList>();
            services.AddTransient<VM.AFTERLOGIN.VMDeviceSelect>();
            services.AddSingleton<VM.VMStartPage>();
            services.AddTransient<VM.VMObservedDeviceCycles>();

            services.AddSingleton<API.Interfaces.IAPIManage, API.APIManage>();
            services.AddSingleton<API.Interfaces.IDeviceController, API.Controllers.v1.DeviceController>();
            services.AddSingleton<API.Interfaces.ISetController, API.Controllers.v1.SetController>();
            services.AddSingleton<API.Interfaces.IOtherController, API.Controllers.v1.OtherController>();
            services.AddSingleton<API.Interfaces.IWOController, API.Controllers.v1.WOController>();
            services.AddSingleton<API.Interfaces.IActController, API.Controllers.v1.ActController>();
            services.AddSingleton<API.Interfaces.IActPerController, API.Controllers.v1.ActPerController>();
            services.AddSingleton<API.Interfaces.IPlanController, API.Controllers.v1.PlanController>();
            services.AddSingleton<API.Interfaces.IPartController, API.Controllers.v1.PartController>();
            services.AddSingleton<API.Interfaces.IIdentityController, API.Controllers.IdentityController>();
            services.AddSingleton<API.Interfaces.IUserController, API.Controllers.v1.UserController>();

            //services.AddHttpClient("CMM", (service, client) =>
            //{
            //    var apiMan = service.GetService<CMMMobileMaui.API.Interfaces.IAPIManage>();

            //    if(apiMan != null
            //    && apiMan.IsDataSet())
            //    {
            //        client.BaseAddress = apiMan.GetHostUri();
            //        client.Timeout = new TimeSpan(0, 0, 10);
            //        client.DefaultRequestHeaders.Accept.Clear();
            //        client.DefaultRequestHeaders.Accept.Add(
            //            new MediaTypeWithQualityHeaderValue("application/json"));

            //        if(apiMan.IsSSL)
            //        {
            //            client.DefaultRequestHeaders.Add()
            //        }
            //    }
            //});


            services.AddTransient<VM.VMDeviceLocationManager>();
            services.AddTransient<BIANOR.VMMouldCalendarChange>();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.CreateScope();

            API.MainObjects.Instance.ServiceProvider = serviceProvider;
        }
    }
}
