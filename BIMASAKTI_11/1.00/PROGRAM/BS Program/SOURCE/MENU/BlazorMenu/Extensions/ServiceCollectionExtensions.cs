using BlazorClientHelper;
using Blazored.LocalStorage;
using BlazorMenu.Authentication;
using BlazorMenu.Constants.Storage;
using BlazorMenu.JSInterop;
using BlazorMenu.Managers;
using BlazorMenu.Middlewares;
using BlazorMenu.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using R_APIClient;
using R_AuthenticationEnumAndInterface;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Configurations;
using R_BlazorFrontEnd.State;
using R_ContextFrontEnd;
using R_CrossPlatformSecurity;
using System.Globalization;
using Telerik.DataSource.Extensions;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace BlazorMenu.Extensions
{
    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection R_AddBlazorFrontEnd(this IServiceCollection services)
        {
            services.AddScoped<R_ITokenRepository, BlazorMenuTokenRepository>();
            services.AddSingleton<R_ContextHeader>();

            services.AddTransient<R_HttpMessageHandler>();
            services.AddTransient<R_HttpInterceptor>();
            services.AddHttpClientInterceptor();

            services.AddSingleton<R_IMenuService, R_MenuService>();
            services.AddSingleton<R_ILoginService, R_LoginService>();

            services.AddSingleton<R_AccessStateContainer>();
            services.AddSingleton<R_BlazorMenuJsInterop>();
            services.AddSingleton<R_ISymmetricJSProvider, R_SymmetricAesJsProvider>();

            var loUrlSections = R_FrontConfig.R_GetServiceUrlSection();

            foreach (var loUrl in loUrlSections)
            {
                services.AddHttpClient(loUrl.ServiceUrlName, client =>
                {
                    client.BaseAddress = new Uri(loUrl.ServiceUrl);
                    client.Timeout = TimeSpan.FromMinutes(10);
                }).AddHttpMessageHandler<R_HttpMessageHandler>();
            }

            services.AddSingleton<IClientHelper, U_GlobalVar>();

            return services;
        }

        internal static async Task R_UseBlazorFrontEnd(this WebAssemblyHost host)
        {
            var contextHeader = host.Services.GetRequiredService<R_ContextHeader>();
            var httpClientFactory = host.Services.GetRequiredService<IHttpClientFactory>();
            var loUrlSections = R_FrontConfig.R_GetServiceUrlSection();

            foreach (var loUrl in loUrlSections)
            {
                var httpClient = httpClientFactory.CreateClient(loUrl.ServiceUrlName).EnableIntercept(host.Services);
                var loTokenRepository = host.Services.GetRequiredService<R_ITokenRepository>();

                if (loUrl.ServiceUrlName.Equals("R_TokenServiceUrl", StringComparison.InvariantCultureIgnoreCase))
                {
                    R_HTTPClient.R_CreateInstanceWithName(loUrl.ServiceUrlName, httpClient, poTokenRepository: loTokenRepository);
                    continue;
                }

                R_HTTPClient.R_CreateInstanceWithName(loUrl.ServiceUrlName, httpClient, contextHeader, loTokenRepository);
            }

            var loLocalStorage = host.Services.GetRequiredService<ILocalStorageService>();
            var lcCulture = await loLocalStorage.GetItemAsStringAsync(StorageConstants.Culture);

            CultureInfo loCulture = new CultureInfo("en");
            if (!string.IsNullOrWhiteSpace(lcCulture))
                loCulture = new CultureInfo(lcCulture);

            CultureInfo.DefaultThreadCurrentCulture = loCulture;
            CultureInfo.DefaultThreadCurrentUICulture = loCulture;

            var loFrontContext = new R_FrontContext(contextHeader.R_Context);
        }

        internal static IServiceCollection AddUIServices(this IServiceCollection services)
        {
            services.AddManagers();

            return services;
        }

        private static IServiceCollection AddManagers(this IServiceCollection services)
        {
            var managers = typeof(IManager);

            var types = managers
                .Assembly
                .GetExportedTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Service = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .Where(t => t.Service != null);

            foreach (var type in types)
            {
                if (managers.IsAssignableFrom(type.Service))
                    services.AddTransient(type.Service, type.Implementation);
            }

            return services;
        }
    }
}
