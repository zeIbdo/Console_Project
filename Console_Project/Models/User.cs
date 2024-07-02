namespace Console_Project.Models;

public class User:BaseEntity
{
    private static int id;
    public string FullName { get; set; }
    public string Email {  get; set; }
    public string Password { get; set; }
    public User(string fullname,string mail,string password)
    {
        Id = ++id;
        FullName = fullname;
        Email = mail;
        Password = password;
    }
  
}
