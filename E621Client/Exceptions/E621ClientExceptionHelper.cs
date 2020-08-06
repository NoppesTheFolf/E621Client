using Flurl.Http;
using System;
using System.Net;

namespace Noppes.E621
{
    internal static class E621ClientExceptionHelper
    {
        public static Exception FromException(Exception exception)
        {
            if (!(exception is FlurlHttpException httpException))
                return exception;

            if (httpException.Call.Response == null)
                return exception;

            return httpException.Call.Response.StatusCode switch
            {
                HttpStatusCode.Unauthorized => E621ClientUnauthorizedException.Create(exception),
                HttpStatusCode.Forbidden => E621ClientForbiddenException.Create(exception),
                _ => exception
            };
        }
    }
}
