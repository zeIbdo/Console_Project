namespace Console_Project.Exceptions;

public class SameCategoryException:Exception
{
    public SameCategoryException(string message) : base(message) { }
}
