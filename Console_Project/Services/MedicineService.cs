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
            if (item.Id == medicine.Id)
            {
                Array.Resize(ref DB.Medicines, DB.Medicines.Length + 1);
                DB.Medicines[DB.Medicines.Length - 1] = medicine;
                return;
            }
                    }
        throw new NotFoundException("Category tapilmadi");

    }
    public Medicine[] GetAllMedicines()
    {
        return DB.Medicines;          
    }
    public Medicine GetMedicineById(int id)
    {
        foreach(var item in DB.Medicines)
        {
            if (item.Id == id)
            {
                return item;
            }
        }
        return null;
    }
    public Medicine GetMedicineByName(string name)
    {
        foreach(var item in DB.Medicines) { if (item.Name == name)
            {
                return item;
            } }
        return null;
    }
    public void GetMedicineByCategory(int categoryId)
    {
        foreach (var item in DB.Medicines)
        {
            if(item.CategoryId == categoryId)
            {
                Console.WriteLine(item.ToString);
            }
        }
    }
    public void RemoveMedicine(int id)
    {
        for (int i = 0; i <DB.Medicines.Length; i++)
        {
            if (DB.Medicines[i].Id == id)
            {
                for(int j = id; j< DB.Medicines.Length-1; j++)
                {
                    DB.Medicines[j] = DB.Medicines[j+1];
                }
                Array.Resize(ref DB.Medicines, DB.Medicines.Length-1);
                return;                
            }            
        }
        throw new NotFoundException("id tapilmadi");
    }
    public void UpdateMedicine(int id,Medicine medicine)
    {
        for (int i = 0; i < DB.Medicines.Length; i++)
        {
            if (DB.Medicines[i].Id == id)
            {
                DB.Medicines[i]= medicine;
                return;
            }

        }
        throw new NotFoundException("Tapilmadi");
    }
}
