﻿using booking_car_app.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace booking_car_app.Middlewares
{
    public class UserMiddleware
    {
        private readonly RequestDelegate _next;

        public UserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IHttpClientFactory httpClientFactory)
        {
            // Example: Setting a user manually
            var httpClient = httpClientFactory.CreateClient("IDPClient");

            var request = new HttpRequestMessage(HttpMethod.Get, "/connect/userinfo");

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(content);
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim("Id", user.Id),
            new Claim("UserName", user.UserName),
            new Claim(ClaimTypes.Role, user.Role)

        };

                var identity = new ClaimsIdentity(claims);
                var principal = new ClaimsPrincipal(identity);

                context.User = principal;
            }
            
           

            await _next(context);
        }
    }
}
