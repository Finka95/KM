using GraphQlService.BLL.Exceptions;
using GraphQlService.Constants;

namespace GraphQlService.Filters
{
    public class ErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            if (error.Exception is NotFoundException)
            {
                return error.WithCode(ErrorCodesConstants.NotFound);
            }
            return error;
        }
    }
}
