using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XafBlazorCustomAuth.Blazor.Server
{
    public class CustomAuthentication : AuthenticationStandard
    {
        private AuthenticationStandardLogonParameters customLogonParameters;
        public CustomAuthentication(Type userType, Type logonParametersType) :
            base(userType, logonParametersType)
        {
            customLogonParameters = new AuthenticationStandardLogonParameters();
        }
        public override void Logoff()
        {
            base.Logoff();
            customLogonParameters = new AuthenticationStandardLogonParameters();
        }
        public override void ClearSecuredLogonParameters()
        {
            customLogonParameters.Password = "";
            base.ClearSecuredLogonParameters();
        }
        public override object Authenticate(IObjectSpace objectSpace)
        {
            PermissionPolicyUser result = null;
            string userName = customLogonParameters.UserName;
            string password = customLogonParameters.Password;

             result = objectSpace.FindObject<PermissionPolicyUser>(new BinaryOperator("UserName", userName));

           

            //TODO here you need to validate your user authentication
            //result = (PermissionPolicyUser)base.Authenticate(objectSpace);

            if (!result.ComparePassword(password))
                throw new AuthenticationException("Error");


           

            return result;

          
        }

        public override void SetLogonParameters(object logonParameters)
        {
            this.customLogonParameters = (AuthenticationStandardLogonParameters)logonParameters;
            //base.SetLogonParameters(logonParameters);
        }
        public override object LogonParameters
        {
            get { return customLogonParameters; }
        }
        public override bool IsLogoffEnabled
        {
            get { return true; }
        }
    }



}
