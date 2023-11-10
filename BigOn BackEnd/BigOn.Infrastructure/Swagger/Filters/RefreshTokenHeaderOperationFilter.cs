﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Swagger.Filters
{
    public class RefreshTokenHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.MethodInfo.Name.Equals("RefreshToken"))
            {
                return;
            }
            operation.Parameters ??= new List<OpenApiParameter>();
            var parameter = operation.Parameters.FirstOrDefault(m=>m.In == ParameterLocation.Header
            && m.Name.Equals("request"));

            if (parameter != null)
            {
                operation.Parameters.Remove(parameter);
                parameter = new OpenApiParameter
                {
                    Name = "token",
                    Description = "Refresh Token",
                    AllowEmptyValue = false,
                    Required = true,
                    In  = ParameterLocation.Header,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Title = "Refresh Token"
                    }
                };
                operation.Parameters.Add(parameter);
            }
        }
    }
}
