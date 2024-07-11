using HotChocolate.AspNetCore.Serialization;
using HotChocolate.Execution;
using System.Net;
using GraphQlService.Constants;

namespace GraphQlService
{
    public class CustomHttpResponseFormatter : DefaultHttpResponseFormatter
    {
        protected override HttpStatusCode OnDetermineStatusCode(
            IQueryResult result, FormatInfo format,
            HttpStatusCode? proposedStatusCode)
        {
            if (result.Errors?.Count > 0)
            {
                if(result.Errors.Any(error => error.Code == ErrorCodesConstants.Unauthorized))
                    return HttpStatusCode.Unauthorized;

                if (result.Errors.Any(error => error.Code == ErrorCodesConstants.NotFound))
                    return HttpStatusCode.NotFound;
            }

            return base.OnDetermineStatusCode(result, format, proposedStatusCode);
        }
    }
}
