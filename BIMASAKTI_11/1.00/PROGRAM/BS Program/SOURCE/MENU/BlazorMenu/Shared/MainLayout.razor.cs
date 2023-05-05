using BlazorMenu.Authentication;
using BlazorMenu.Middlewares;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorMenu.Shared
{
    public partial class MainLayout : LayoutComponentBase, IDisposable
    {
        //[Inject] private ILocalStorageService _localStorageService { get; set; }
        [Inject] private AuthenticationStateProvider _stateProvider { get; set; }
        [Inject] private R_HttpInterceptor _interceptor { get; set; }

        public void Dispose()
        {
            _interceptor.DisposeEvent();
        }

        private async Task Logout()
        {
            await ((BlazorMenuAuthenticationStateProvider)_stateProvider).MarkUserAsLoggedOut();

            _navigationManager.NavigateTo("/");
        }

        protected override void OnInitialized()
        {
            _interceptor.RegisterEvent();
        }
    }
}
