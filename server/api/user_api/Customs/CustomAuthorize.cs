using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace user_api.Customs
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class IdentityServerAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
       
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var accessToken = context.HttpContext.Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(accessToken))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (!IsValidToken(accessToken).Result)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }

        private async Task<bool> IsValidToken(string accessToken)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    accessToken = accessToken.Replace("Bearer", string.Empty).Trim();
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    client.SetBearerToken(accessToken);
                    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
                    var response = await client.GetAsync("https://localhost:44342/connect/userinfo");

                    return response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }

}
