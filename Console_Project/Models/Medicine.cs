﻿namespace Console_Project.Models;

public class Medicine:BaseEntity
{
    private static int id;
    public string Name { get; set; }
    public double Price { get; set; }
    public int CategoryId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedTime { get; set; }
    public Medicine(string name,double price,int catId,int usId)
    {
        CreatedTime = DateTime.Now;
        Name = name;
        Price = price;
        CategoryId = catId;
        UserId = usId;
        Id = ++id;
            
    }
    public override string ToString()
    {
        return $"{Name} {Price} {CreatedTime.ToShortDateString()}";
    }
}
