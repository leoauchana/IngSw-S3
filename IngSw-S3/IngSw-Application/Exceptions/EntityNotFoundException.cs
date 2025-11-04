namespace IngSw_Application.Exceptions;

public class EntityNotFoundException : ApplicationExceptions
{
	public EntityNotFoundException(string message) : base(message) { }
}
