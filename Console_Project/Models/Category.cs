namespace Console_Project.Models;

public class Category:BaseEntity
{
    private static int id;

    public string Name { get; set; }
    public Category(string name)
    {
        Name = name;
        Id = ++id;
    }
    public override string ToString()
    {
        return $"{Id} {Name}";
    }
}
