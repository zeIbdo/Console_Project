using Console_Project.Exceptions;
using Console_Project.Models;

namespace Console_Project.Services;

public class CategoryService
{
    public void CreateCategory(Category category)
    {
        foreach(var item in DB.Categories)
        {
            if (item.Name == category.Name)
                throw new SameCategoryException("Bu kateqoriya artiq var");
        }
        Array.Resize(ref DB.Categories, DB.Categories.Length + 1);
        DB.Categories[DB.Categories.Length - 1] = category;
    }
}
