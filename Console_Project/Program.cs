using Console_Project.Exceptions;
using Console_Project.Models;
using Console_Project.Services;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;

namespace Console_Project;

internal class Program
{
    static void Main(string[] args)
    {
        
            CategoryService categoryService = new CategoryService();
            MedicineService medicineService = new MedicineService();
            UserService userService = new UserService();
            User activeUser = new("", "", "");
        Login:
            Console.WriteLine("1.Register\n" +
                "2.Login\n" +
                "3.Exit\n");
            string input = Console.ReadLine();
            switch (input)
            {
                case "0":
                    return;
                case "1":
                    try
                    {
                    Console.WriteLine("fullname:");
                    string fullName = Console.ReadLine();

                Email:
                    Console.WriteLine("mail:");
                    string eMail = Console.ReadLine();

                    if (!IsValidEmail(eMail))
                    {
                        Console.WriteLine("emaili duzgun formatta daxil et");
                        goto Email;
                    }
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
                    Console.WriteLine("press enter");
                    Console.ReadKey();
                    goto Login;
                    }
                    catch (SameMailException e)
                    {

                        Console.WriteLine(e.Message);
                    Console.WriteLine("press enter");
                    Console.ReadKey();
                    goto Login;
                    }
                case "2":
                    try
                    {


                        Console.WriteLine("email:");
                        string mail = Console.ReadLine();
                        Console.WriteLine("password");
                        string pass = Console.ReadLine();
                      activeUser=  userService.Login(mail, pass);
                    }
                    catch (NotFoundException e)
                    {

                        Console.WriteLine(e.Message);
                    Console.WriteLine("press enter");
                    Console.ReadKey();
                    goto Login;
                    }
                    
                    break;
                default:
                    Console.WriteLine("sehv input");
                Console.WriteLine("press enter");
                Console.ReadKey();
                goto Login;
            }
        Menu:
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Create Category");
            Console.WriteLine("2. Create Medicine");
            Console.WriteLine("3. List All Medicines");
            Console.WriteLine("4. Get Medicine By Id");
            Console.WriteLine("5. Get Medicine By Name");
            Console.WriteLine("6. Get Medicine By Category");
            Console.WriteLine("7. Remove Medicine");
            Console.WriteLine("8. Update Medicine");
            Console.WriteLine("9. Go to Login menu");
            Console.WriteLine("10. List All Categories");

            Console.Write("Choose an option: ");
            string option = Console.ReadLine();
            switch (option)
            {
                case "9":
                    goto Login;
                case "10":
                if (DB.Categories.Length == 0) { Console.WriteLine("Hec bir kategoriya yoxdur"); }
                else
                {
                    foreach (var item in DB.Categories)
                    {
                        Console.WriteLine(item);
                    }
                }
                Console.WriteLine("press enter");
                Console.ReadKey();
                goto Menu;
                case "1":
                try
                {
                    Console.WriteLine("name of the category:");
                    string categoryName = Console.ReadLine();
                    Category category = new(categoryName);
                    categoryService.CreateCategory(category);
                    Console.WriteLine($"{categoryName} yaradildi");
                    Console.WriteLine("press enter");
                    Console.ReadKey();
                    goto Menu;
                }
                catch (SameCategoryException e)
                {

                    Console.WriteLine(e.Message);
                    Console.WriteLine("press enter");
                    Console.ReadKey();
                    goto Menu;
                }
                case "2":
                try
                {
                    Console.WriteLine("Medicine Name:");
                    string medicineName = Console.ReadLine();
                price:
                    Console.WriteLine("Price:");
                    double price;
                    string str = Console.ReadLine();
                    bool correctPrice = double.TryParse(str, out price);
                    if (!(correctPrice && price >= 0)) { Console.WriteLine("qiymeti duzgun daxil et"); ; goto price; }
                    Console.WriteLine("Id of Category which you want:");
                    int categoryId = int.Parse(Console.ReadLine());
                    Medicine medicine = new(medicineName, price, categoryId, activeUser.Id);
                    medicineService.CreateMedicine(medicine);
                    Console.WriteLine($"{medicineName} yaradildi");
                    Console.WriteLine("press enter");
                    Console.ReadKey();
                    goto Menu;
                }
                catch (NotFoundException e)
                {

                    Console.WriteLine(e.Message);
                    Console.WriteLine("press enter");
                    Console.ReadKey();
                    goto Menu;
                }
                case "3":
                if (medicineService.GetAllMedicines(activeUser.Id).Length == 0) { Console.WriteLine("medicine yaratmamisiniz"); }
                else
                {
                    foreach (Medicine item in medicineService.GetAllMedicines(activeUser.Id))
                    {
                        Console.WriteLine($"{item}");
                    }
                }
                Console.WriteLine("press enter");
                Console.ReadKey();
                goto Menu;
                case "4":
                try
                {
                Id:
                    Console.WriteLine("Id of Medicine which you want:");
                    int medicineId;
                    string str = Console.ReadLine();
                    bool correctId = int.TryParse(str, out medicineId);
                    if (!(correctId && medicineId >= 0)) { Console.WriteLine("qiymeti duzgun daxil et"); ; goto Id; }
                    Console.WriteLine(medicineService.GetMedicineById(medicineId));
                    goto Menu;
                }
                catch (NotFoundException e)
                {

                    Console.WriteLine(e.Message) ;
                    Console.WriteLine("press enter");
                    Console.ReadKey();
                    goto Menu;
                }
                case "5":
                try
                {
                    Console.WriteLine("Name of Medicine which you want:");
                    string nameOfMedicine = Console.ReadLine();
                    Console.WriteLine(medicineService.GetMedicineByName(nameOfMedicine));
                    Console.WriteLine("press enter");
                    Console.ReadKey();
                    goto Menu;
                }
                catch (NotFoundException e)
                {

                    Console.WriteLine(e.Message);
                    Console.WriteLine("press enter");
                    Console.ReadKey();
                    goto Menu;
                }
                case "6":
                try
                {
                    Id:
                    Console.WriteLine("category id of medicines what you want:");
                    int idOfCategory;
                    string str = Console.ReadLine() ;
                    bool correctId  = int.TryParse(str, out idOfCategory);
                    if (!(correctId && idOfCategory >= 0)) { Console.WriteLine("qiymeti duzgun daxil et"); ; goto Id; }
                    medicineService.GetMedicineByCategory(idOfCategory);
                    Console.WriteLine("press enter");
                    Console.ReadKey();
                    goto Menu;
                }
                catch (NotFoundException e)
                {

                    Console.WriteLine(e.Message);
                    Console.WriteLine("press enter");
                    Console.ReadKey();
                    goto Menu;
                }
                case "7":
                try
                {
                Id:
                    Console.WriteLine("id of medicine which you want to remove:");
                    int medicineId;               
                    string str = Console.ReadLine();
                    bool correctId = int.TryParse(str, out medicineId);
                    if (!(correctId && medicineId >= 0)) { Console.WriteLine("qiymeti duzgun daxil et"); ; goto Id; }                    
                    medicineService.RemoveMedicine(medicineId);
                    Console.WriteLine("press enter");
                    Console.ReadKey();
                    goto Menu;
                }
                catch (NotFoundException e)
                {

                    Console.WriteLine(e.Message);
                    Console.WriteLine("press enter");
                    Console.ReadKey();
                    goto Menu;
                }
                case "8":
                try
                {
                Id:
                    Console.WriteLine("id of medicine which you want to update");
                    int medicineId;
                    string str = Console.ReadLine();
                    bool correctId = int.TryParse(str, out medicineId);
                    if (!(correctId && medicineId >= 0)) { Console.WriteLine("qiymeti duzgun daxil et"); ; goto Id; }
                    Console.WriteLine("name:");
                    string nameOfMedicine = Console.ReadLine();
                Price:
                    Console.WriteLine("Price:");
                    double price2;
                    string str2 = Console.ReadLine();
                    bool correctId2 = double.TryParse(str, out price2);
                    if (!(correctId && price2 >= 0)) { Console.WriteLine("qiymeti duzgun daxil et"); ; goto Id; }
                Id2:
                    Console.WriteLine("category id of medicine:");
                    int categoryId2;
                    string str3 = Console.ReadLine();                    
                    bool correctId3 = int.TryParse(str, out categoryId2);
                    if (!(correctId && categoryId2 >= 0)) { Console.WriteLine("qiymeti duzgun daxil et"); ; goto Id; }
                    Medicine medicine2 = new(nameOfMedicine, price2, categoryId2, activeUser.Id);
                    medicineService.UpdateMedicine(medicineId, medicine2);
                    Console.WriteLine("press enter");
                    Console.ReadKey();
                    goto Menu;
                }
                catch (NotFoundException e)
                {

                    Console.WriteLine(e.Message);
                    Console.WriteLine("press enter");
                    Console.ReadKey();
                    goto Menu;
                }
                default:
                    Console.WriteLine("invalid input");
                Console.WriteLine("press enter");
                Console.ReadKey();
                goto Menu;
            }



        
    }
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;


        string pattern = @"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$";
        Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

        return regex.IsMatch(email);
    }
}
