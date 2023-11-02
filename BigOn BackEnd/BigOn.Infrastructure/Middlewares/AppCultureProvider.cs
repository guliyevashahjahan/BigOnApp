﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Middlewares
{
    public class AppCultureProvider : RequestCultureProvider
    {
        public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
        {
            string lang = "az";

            if (httpContext is null)
                throw new ArgumentNullException(nameof(httpContext));

            if (httpContext.Request.Headers.ContainsKey("lang") &&
                httpContext.Request.Headers.TryGetValue("lang",out StringValues values) &&
                values.Any())
            {
                if (Regex.IsMatch(values.First(), @"^(en|az|ru)$"))
                    lang = values.First();
            }
            return Task.FromResult(new ProviderCultureResult(lang));
        }
    }
}