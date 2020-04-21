using EnvelhecerBem.Core;
using Flurl;
using Microsoft.AspNetCore.Http;

namespace EnvelhecerBem.Api
{
    public static class UrlExtensions
    {
        public static Url GetLocation(this IEntity entity, HttpRequest request)
        {
            var path = request.Path.ToString();
            var url = path.AppendPathSegment(entity.Id);
            return url;
        }
    }
}