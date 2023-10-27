using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Extensions
{
    public static partial class Extension
    {
        public static string GetHost(this HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host}";
        }
        public static string GetHeaderValue(this HttpRequest request, string key)
        {
            if (request.Headers.TryGetValue(key,out StringValues values))
                return values.First();

            return null;
        }
        public static IDictionary<string, object> AppendHeaderTo(this HttpRequest request,IDictionary<string,object> items, string key)
        {
            if (request.Headers.TryGetValue(key, out StringValues values))
                items.Add(key, values.First());

            return items;
        }
    }
}
