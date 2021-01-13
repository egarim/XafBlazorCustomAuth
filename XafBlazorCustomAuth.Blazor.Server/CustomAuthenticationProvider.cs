using DevExpress.ExpressApp.Security;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace XafBlazorCustomAuth.Blazor.Server
{
    public class CustomAuthenticationProvider : AuthenticationStandardProviderV2
    {

        public CustomAuthenticationProvider(IOptions<AuthenticationStandardProviderOptions> options, IOptions<SecurityOptions> securityOptions) :
            base(options, securityOptions)
        {

        }

        protected override AuthenticationBase CreateAuthentication(Type userType, Type logonParametersType)
        {

            return new CustomAuthentication(userType, logonParametersType);
        }
    }
}
