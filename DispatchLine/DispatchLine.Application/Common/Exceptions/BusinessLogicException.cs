namespace DispatchLine.Application.Common.Exceptions;

public class BusinessLogicException: Exception
{
    public BusinessLogicError[] Errors { get; set; }

    public BusinessLogicException(string code, string description)
    {
        Errors = new[] { new BusinessLogicError(code, description) };
    }

    public BusinessLogicException(string code, string description, string detail)
    {
        Errors = new[] { new BusinessLogicError(code, description, detail) };
    }
    
    public BusinessLogicException(BusinessLogicError error)
    {
        Errors = new[] { error };
    }

    public BusinessLogicException(IEnumerable<BusinessLogicError> errors)
    {
        Errors = errors.ToArray();
    }
}

public class BusinessLogicError
{
    /// <example>422-Departments-01</example>
    public string Code { get; set; }
    
    /// <example>INVALID_VALUE</example>
    public string Description { get; set; }
    
    /// <example>Varsa detay açıklama bu alanda görünür.</example>
    public string? Detail { get; set; }

    public BusinessLogicError(string code, string description)
    {
        Code = code;
        Description = description;
    }

    public BusinessLogicError(string code, string description, string detail)
    {
        Code = code;
        Description = description;
        Detail = detail;
    }
}

public class BusinessLogicResult
{
    public BusinessLogicResult(bool succeeded, IEnumerable<BusinessLogicError> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public bool Succeeded { get; set; }

    public BusinessLogicError[] Errors { get; set; }

    public static BusinessLogicResult Success()
    {
        return new BusinessLogicResult(true, new List<BusinessLogicError>());
    }

    public static BusinessLogicResult Failure(IEnumerable<BusinessLogicError> errors)
    {
        return new BusinessLogicResult(false, errors);
    }
}
