using Console_Project.Models;
using Console_Project.Exceptions;
using System.Text.RegularExpressions;

namespace Console_Project.Services;

public class UserService
{
   public User Login(string email,string password)
    {
        foreach(User user in DB.Users)
        {
            if(user.Email == email)
            {
                if (user.Password == password)
                {
                    return user;
                }
            }
        }
        throw new NotFoundException("email ve yaxud password sehvdir");
    }
    public void AddUser(User user)
    {

        foreach (var item in DB.Users)
        {
            if (item.Email == user.Email)
            {
                throw new SameMailException("bu mail ile user artiq var");
            }
        }
        Array.Resize(ref DB.Users, DB.Users.Length+1);
        DB.Users[DB.Users.Length-1] = user;
    }
   
}
