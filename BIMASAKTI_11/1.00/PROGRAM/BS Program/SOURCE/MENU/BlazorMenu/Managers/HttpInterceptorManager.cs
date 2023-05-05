using Microsoft.AspNetCore.Components;

namespace BlazorMenu.Managers
{
    public class HttpInterceptorManager
    {
        private readonly HttpClientInterceptor _interceptor;
        private readonly NavigationManager _navigationManager;

        public HttpInterceptorManager(
            HttpClientInterceptor interceptor,
            NavigationManager navigationManager)
        {
            _interceptor = interceptor;
            _navigationManager = navigationManager;
        }

        public void DisposeEvent()
        {
            _interceptor.BeforeSendAsync -= InterceptBeforeHttpAsync;
        }

        public Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e)
        {
            var absPath = e.Request.RequestUri.AbsolutePath;
            //if (!absPath.Contains("token") && !absPath.Contains("accounts"))
            //{
            //    try
            //    {
            //        var token = await _authenticationManager.TryRefreshToken();
            //        if (!string.IsNullOrEmpty(token))
            //        {
            //            e.Request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        await _authenticationManager.Logout();
            //        _navigationManager.NavigateTo("/");
            //    }
            //}

            return Task.CompletedTask;
        }

        public void RegisterEvent()
        {
            _interceptor.BeforeSendAsync += InterceptBeforeHttpAsync;
        }
    }
}
