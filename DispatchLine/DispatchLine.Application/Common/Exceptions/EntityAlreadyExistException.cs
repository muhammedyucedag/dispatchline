namespace DispatchLine.Application.Common.Exceptions;

public class EntityAlreadyExistException : Exception
{
    public EntityAlreadyExistException(string message) : base(message)
    {
    }

    public EntityAlreadyExistException(string objectName, string key) : base($"Queried object {objectName} already exist, Key: {key}")
    {
    }
}
