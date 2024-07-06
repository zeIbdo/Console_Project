using Console_Project.Exceptions;
using Console_Project.Models;
using System.Collections;

namespace Console_Project.Services;

public class MedicineService
{
    public void CreateMedicine(Medicine medicine)
    {
        foreach (var item in DB.Categories)
        {
            if (item.Id == medicine.CategoryId)
            {
                Array.Resize(ref DB.Medicines, DB.Medicines.Length + 1);
                DB.Medicines[DB.Medicines.Length - 1] = medicine;
                return;
            }
                    }
        throw new NotFoundException("Category tapilmadi");

    }
    public Medicine[] GetAllMedicines(int userId)
    {
        foreach(var item in DB.Users)
        {
            if (item.Id == userId)
            {
                return DB.Medicines;
            }
        }
        throw new NotFoundException("user tapilmadi");
    }
    public Medicine GetMedicineById(int id,int userIdd)
    {
        foreach(var item in GetAllMedicines(userIdd))
        {
            if (item.Id == id)
            {
                return item;
            }
        }
        throw new NotFoundException("derman tapilmadi");
    }
    public Medicine GetMedicineByName(string name, int userIdd)
    {
        foreach(var item in GetAllMedicines(userIdd)) { if (item.Name == name)
            {
                return item;
            } }
        throw new NotFoundException("derman tapilmadi");
    }
    public void GetMedicineByCategory(int categoryId,int userIdd)
    {
        bool found = false;
        foreach (var item in GetAllMedicines(userIdd))
        {
            if(item.CategoryId == categoryId)
            {
                found = true;
                Console.WriteLine(item);
            }
        }
        if (found==false) { throw new NotFoundException("derman tapilmadi"); }
    }
    public void RemoveMedicine(int id,int userIdd)
    {
        var medicines = GetAllMedicines(userIdd);
        for (int i = 0; i <medicines.Length; i++)
        {
            if (medicines[i].Id == id)
            {
                for(int j = id; j< medicines.Length - 1; j++)
                {
                    medicines[j] = medicines[j+1];
                }
                Array.Resize(ref medicines, medicines.Length-1);
                return;                
            }            
        }
        throw new NotFoundException("derman tapilmadi");
    }
    public void UpdateMedicine(int id,int userIdd,Medicine medicine)
    {
        var medicines = GetAllMedicines(userIdd);
        for (int i = 0; i < medicines.Length; i++)
        {
            if (medicines[i].Id == id)
            {
                medicines[i]= medicine;
                return;
            }

        }
        throw new NotFoundException("derman Tapilmadi");
    }
}
