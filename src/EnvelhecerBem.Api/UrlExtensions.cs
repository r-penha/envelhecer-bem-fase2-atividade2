using EnvelhecerBem.Core;
using Flurl;
using Microsoft.AspNetCore.Http;

namespace EnvelhecerBem.Api
{
    public static class UrlExtensions
    {
        public static Url GetLocation(this IEntity entity, HttpRequest request) 
            => request.Path.ToString().AppendPathSegment(entity.Id);
    }
}