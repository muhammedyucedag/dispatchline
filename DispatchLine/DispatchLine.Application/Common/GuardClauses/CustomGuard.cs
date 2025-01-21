using Ardalis.GuardClauses;
using DispatchLine.Application.Common.Exceptions;

namespace DispatchLine.Application.Common.GuardClauses;

public static class CustomGuard
{
    public static void AlreadyExist(this IGuardClause guardClause, bool any, string parameterName)
    {
        if (any)
        {
            throw new EntityAlreadyExistException(parameterName);
        }
    }

    public static void UnProcessableEntity(this IGuardClause guardClause, bool predicate, BusinessLogicError businessLogicError)
    {
        if (predicate)
        {
            throw new BusinessLogicException(businessLogicError);
        }
    }
    
    
    public static void UnProcessableEntity(this IGuardClause guardClause, bool predicate, string code , string message)
    {
        if (predicate)
        {
            throw new BusinessLogicException(new BusinessLogicError(code, message));
        }
    }
    
    public static void UnProcessableError(this IGuardClause guardClause, BusinessLogicError businessLogicError)
    {
        throw new BusinessLogicException(businessLogicError);
    }
}
