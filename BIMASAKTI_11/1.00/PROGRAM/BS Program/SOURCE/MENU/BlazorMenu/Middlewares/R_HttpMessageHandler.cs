using R_AuthenticationEnumAndInterface;

namespace BlazorMenu.Middlewares
{
    public class R_HttpMessageHandler : DelegatingHandler
    {
        private readonly R_ITokenRepository _tokenRepository;

        public R_HttpMessageHandler(R_ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //if (_tokenRepository.R_IsTokenExpired())
            //{
            //    var httpClient = R_HTTPClient.R_GetInstanceWithName("R_TokenServiceUrl");

            //    var result = await httpClient.R_APIRequestObject<R_RefreshTokenResultDTO>(pcRequest: R_AuthenticationEnumerationConstant.R_DEFAULT_API_REFRESH_TOKEN_REQUEST_ENDPOINT,
            //                                                 peAPIMethod: R_HTTPClient.E_APIMethod.POST_MODE,
            //                                                 plSendWithContext: false,
            //                                                 plErrorOnBody: false);
            //}

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
