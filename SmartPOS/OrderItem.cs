using System;

public class OrderItem
{

    public int ItemId { get; set; }

    public string ItemName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public decimal TotalPrice => Quantity * Price;


    public OrderItem() { }

    public OrderItem( int ItemId,string ItemName, int Quantity, decimal Price)
	{

        this.ItemId = ItemId;
        this.ItemName = ItemName;
        this.Quantity = Quantity;
        this.Price = Price;
    }

    public override string ToString()
    {
        return $"{ItemName} - {Quantity} x {Price:C} = {TotalPrice:C}";
    }

}
