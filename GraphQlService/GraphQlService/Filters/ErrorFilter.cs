using GraphQlService.BLL.Exceptions;

namespace GraphQlService.Filters
{
    public class ErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            if (error.Exception is NotFoundException)
            {
                return error.WithCode("NOT_FOUND");
            }
            return error;
        }
    }
}
