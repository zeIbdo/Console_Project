using Console_Project.Exceptions;
using Console_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Console_Project.Services
{
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
            throw new NotFoundException("Category not found");
        }

        public IEnumerable<Medicine> GetAllMedicines(int userId)
        {
            bool userFound = false;
            foreach (var user in DB.Users)
            {
                if (user.Id == userId)
                {
                    userFound = true;
                    break;
                }
            }

            if (!userFound)
            {
                throw new NotFoundException("User not found");
            }

            foreach (var medicine in DB.Medicines)
            {
                if (medicine.UserId == userId)
                {
                    yield return medicine;
                }
            }
        }

        public Medicine GetMedicineById(int id, int userId)
        {
            foreach (var medicine in GetAllMedicines(userId))
            {
                if (medicine.Id == id)
                {
                    return medicine;
                }
            }
            throw new NotFoundException("Medicine not found");
        }

        public Medicine GetMedicineByName(string name, int userId)
        {
            foreach (var medicine in GetAllMedicines(userId))
            {
                if (medicine.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return medicine;
                }
            }
            throw new NotFoundException("Medicine not found");
        }

        public void GetMedicineByCategory(int categoryId, int userId)
        {
            bool found = false;
            foreach (var medicine in GetAllMedicines(userId))
            {
                if (medicine.CategoryId == categoryId)
                {
                    found = true;
                    Console.WriteLine(medicine);
                }
            }
            if (!found)
            {
                throw new NotFoundException("No medicines found for the specified category");
            }
        }

        public void RemoveMedicine(int id, int userId)
        {
            var medicines = GetAllMedicines(userId).ToArray();
            int index = -1;
            for (int i = 0; i < medicines.Length; i++)
            {
                if (medicines[i].Id == id)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                throw new NotFoundException("Medicine not found");
            }

            for (int i = index; i < medicines.Length - 1; i++)
            {
                medicines[i] = medicines[i + 1];
            }
            Array.Resize(ref medicines, medicines.Length - 1);

            DB.Medicines = DB.Medicines.Where(m => m.UserId != userId || m.Id != id).ToArray();
        }

        public void UpdateMedicine(int id, int userId, Medicine updatedMedicine)
        {
            var medicines = GetAllMedicines(userId).ToArray();
            bool found = false;
            for (int i = 0; i < medicines.Length; i++)
            {
                if (medicines[i].Id == id)
                {
                    found = true;
                    updatedMedicine.Id = id;
                    medicines[i] = updatedMedicine;

                    for (int j = 0; j < DB.Medicines.Length; j++)
                    {
                        if (DB.Medicines[j].UserId == userId && DB.Medicines[j].Id == id)
                        {
                            DB.Medicines[j] = updatedMedicine;
                            return;
                        }
                    }
                }
            }

            if (!found)
            {
                throw new NotFoundException("Medicine not found");
            }
        }
    }
}
