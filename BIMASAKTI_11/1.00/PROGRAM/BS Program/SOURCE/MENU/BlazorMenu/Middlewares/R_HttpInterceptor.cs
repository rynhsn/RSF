using R_AuthenticationEnumAndInterface;
using Toolbelt.Blazor;

namespace BlazorMenu.Middlewares
{
    public class R_HttpInterceptor
    {
        private readonly HttpClientInterceptor _interceptor;
        private readonly R_ITokenRepository _tokenRepository;
        private readonly IHttpClientFactory _httpClientFactory;

        public R_HttpInterceptor(
            HttpClientInterceptor interceptor,
            R_ITokenRepository tokenRepository,
            IHttpClientFactory httpClientFactory)
        {
            _interceptor = interceptor;
            _tokenRepository = tokenRepository;
            _httpClientFactory = httpClientFactory;
        }

        public void DisposeEvent()
        {
            _interceptor.BeforeSendAsync -= InterceptBeforeHttpAsync;
        }

        public Task InterceptBeforeHttpAsync(object sender, HttpClientInterceptorEventArgs e)
        {
            var absPath = e.Request.RequestUri.AbsolutePath;

            //if (absPath == R_AuthenticationEnumerationConstant.R_DEFAULT_API_REFRESH_TOKEN_REQUEST_ENDPOINT)
            //    return;

            //if (_tokenRepository.R_IsTokenExpired())
            //{
            //    var client = _httpClientFactory.CreateClient("R_TokenServiceUrl");

            //    await client.GetAsync(R_AuthenticationEnumerationConstant.R_DEFAULT_API_REFRESH_TOKEN_REQUEST_ENDPOINT);

            //    //var httpClient = R_HTTPClient.R_GetInstanceWithName("R_TokenServiceUrl");

            //    //var result = await httpClient.R_APIRequestObject<R_RefreshTokenResultDTO>(pcRequest: R_AuthenticationEnumerationConstant.R_DEFAULT_API_REFRESH_TOKEN_REQUEST_ENDPOINT,
            //    //                                             peAPIMethod: R_HTTPClient.E_APIMethod.POST_MODE,
            //    //                                             plSendWithContext: false,
            //    //                                             plErrorOnBody: false);
            //}

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
