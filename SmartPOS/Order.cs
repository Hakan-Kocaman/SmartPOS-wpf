using System;
public class Order
{ // databaseye insert edilecek
  // OrderItems=(bagel,bagel,bagel,tea,tea,tea,tea,tea,lemonade,coffe,pastry)
  // 'insert into Orders values(OrderDate,Employee_id)'
 


    public Decimal TotalPrice { get; set; }
    public int EmployeeId { get; set; }
    public  DateTime OrderDate { get; set; }

    public List<OrderItem> Items { get; set; } = new();
    public int Id { get; set;}
    public string EmployeeName { get; set; }

    public Order() { }

    public Order(Decimal TotalPrice,int EmployeeId, DateTime OrderDate)
	{
 
        this.TotalPrice = TotalPrice;
        this.EmployeeId = EmployeeId;
        this.OrderDate = OrderDate;
    }
}
