using Console_Project.Exceptions;
using Console_Project.Models;
using Console_Project.Services;
using System.ComponentModel.Design;

namespace Console_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CategoryService categoryService = new CategoryService();
            MedicineService medicineService = new MedicineService();
                    UserService userService = new UserService();
            User activeUser = new("", "", "");
            Login:
            Console.WriteLine("1.Register\n"+
                "2.Login\n"+
                "3.Exit\n");
             string input = Console.ReadLine();
            switch (input)
            {
                case "0":
                    return;
                case "1":
                    Console.WriteLine("fullname:");
                    string fullName = Console.ReadLine();
                    Email:
                    bool containsMeat = false;
                    bool containsDot = false;
                    Console.WriteLine("mail:");
                    string eMail = Console.ReadLine();
                    foreach (var item in eMail)
                    {
                        if (item == '.')
                        {
                            containsDot = true;
                        }
                        else if (item == '@') containsMeat = true;
                    }
                    if (!(containsMeat && containsDot)) { Console.WriteLine("emaili duzgun formatta daxil et"); goto Email; }
                Password:
                    bool correctLength = false;
                    bool containsUpper = false;
                    bool containsDigit = false;
                    Console.WriteLine("password:");
                    string password = Console.ReadLine();
                    if (password.Length >= 8) correctLength = true;
                    foreach (var item in password)
                    {
                        if (char.IsUpper(item)) { containsUpper = true; }
                        else if (char.IsDigit(item)) { containsDigit = true; }
                    }
                    if (!(correctLength && containsUpper && containsDigit)) { Console.WriteLine("password 8 char uzunluqda ve passwordda 1 reqem ve 1 boyuk herf olmalidir "); goto Password; }
                    activeUser = new(fullName, eMail, password);
                    userService.AddUser(activeUser);
                    goto Login;
                case "2":
                    try
                    {
                        Console.WriteLine("email:");
                        string mail = Console.ReadLine();                
                        Console.WriteLine("password");
                        string pass = Console.ReadLine();                        
                        userService.Login(mail, pass);
                    }
                    catch (NotFoundException e)
                    {

                        Console.WriteLine(e.Message); ;
                    }
                    break;
                default:
                    Console.WriteLine("sehv input");
                    goto Login;
            }
            Menu:
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Create Category");
            Console.WriteLine("10. List All Categories");
            Console.WriteLine("2. Create Medicine");
            Console.WriteLine("3. List All Medicines");
            Console.WriteLine("4. Get Medicine By Id");
            Console.WriteLine("5. Get Medicine By Name");
            Console.WriteLine("6. Get Medicine By Category");
            Console.WriteLine("7. Remove Medicine");
            Console.WriteLine("8. Update Medicine");
            Console.WriteLine("9. Go to Login");

            Console.Write("Choose an option: ");
            string option = Console.ReadLine();
            switch (option)
            {
                case "9":
                    goto Login;
                case "10":
                    foreach(var item in DB.Categories) {  Console.WriteLine(item); }
                    goto Menu;
                case "1":
                    Console.WriteLine("name of the category:");
                    string categoryName=Console.ReadLine();
                    Category category = new(categoryName);
                    categoryService.CreateCategory(category);
                    goto Menu;
                case "2":
                    Console.WriteLine("Medicine Name:");
                    string medicineName = Console.ReadLine();
                    Console.WriteLine("Price:");
                    double price = double.Parse(Console.ReadLine());
                    Console.WriteLine("Id of Category which you want:");
                    int categoryId= int.Parse(Console.ReadLine());
                    Medicine medicine = new(medicineName, price,categoryId , activeUser.Id);
                    medicineService.CreateMedicine(medicine);
                    goto Menu;
                case "3":
                    foreach(Medicine item  in medicineService.GetAllMedicines()) {  Console.WriteLine(item); }
                    goto Menu;
                case "4":
                    Console.WriteLine("Id of Medicine which you want:");
                    int medicineId= int.Parse(Console.ReadLine());
                    Console.WriteLine( medicineService.GetMedicineById(medicineId));
                    goto Menu;
                case "5":
                    Console.WriteLine("Name of Medicine which you want:");
                    string nameOfMedicine = Console.ReadLine();
                    Console.WriteLine(medicineService.GetMedicineByName(nameOfMedicine));
                    goto Menu;
                case "6":
                    Console.WriteLine("category id of medicines what you want:");
                    int idOfCategory = int.Parse(Console.ReadLine());
                    medicineService.GetMedicineByCategory(idOfCategory);
                    goto Menu;
                case "7":
                    Console.WriteLine("id of medicine which you want to remove:");
                    medicineId = int.Parse(Console.ReadLine());
                    medicineService.RemoveMedicine(medicineId);
                    goto Menu;
                case "8":
                    Console.WriteLine("id of medicine which you want to update");
                    medicineId=int.Parse(Console.ReadLine());
                    Console.WriteLine("name:");
                    nameOfMedicine= Console.ReadLine();
                    Console.WriteLine("Price:");
                    price = double.Parse(Console.ReadLine());
                    Console.WriteLine("category id of medicine:");
                    categoryId = int.Parse(Console.ReadLine());
                    medicine = new(nameOfMedicine, price, categoryId, activeUser.Id);
                    medicineService.UpdateMedicine(medicineId, medicine);
                    goto Menu;

            }



        }
    }
}
