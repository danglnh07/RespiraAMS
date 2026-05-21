namespace RespiraAMS.Core.Exceptions;

public class NotFoundException(string entity, Guid id) : Exception($"{entity} with this ID not found: {id}")
{
    
}