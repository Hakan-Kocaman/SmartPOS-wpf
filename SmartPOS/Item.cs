using System;

public class Item
{ // databaseden çekiliyor
	public int Id { get; set; }
	public string Name { get; set; }
	public decimal Price { get; set; }
	public int Category_Id { get; set; }

	

   
	public Item(int Id,string Name,decimal Price,int Category_Id)
	{
		this.Id = Id;
		this.Name = Name;
		this.Price = Price;
		this.Category_Id = Category_Id;


    }



   

}


