namespace StateService.Api.Handlers
{
    class AuthHeaderHandler : DelegatingHandler
    {
        private readonly IHeaderDictionary _headers;


        public AuthHeaderHandler(IHeaderDictionary headers)
        {
            _headers = headers ?? throw new ArgumentNullException(nameof(headers));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_headers.Authorization.Any())
            {
                request.Headers.Add("Authorization", _headers.Authorization.ToString());
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}