using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;


public class SQLManager
{
    private static readonly string _connectionString =
     "Server=KOCAMAN\\SQLEXPRESS01;Database=OTOMATIONDB;Trusted_Connection=True;Encrypt=False;";


    public static void TestConn()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                MessageBox.Show("Database Connected");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

    }

    public static SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }

    public static string GetCompanyTitle()
    {

        using var conn = GetConnection();
        using var cmd = new SqlCommand("SELECT Company_title FROM Company WHERE Company_id=1", conn);

        conn.Open();
        var result = cmd.ExecuteScalar();

        return result?.ToString();
    }
    public static int GetEmployeeId(string name)
    {

        try
        {
            using var conn = GetConnection();
            using var cmd = new SqlCommand("SELECT Employee_id FROM Employee Where Employee_name ='" + name + "'", conn);

            conn.Open();
            var id = cmd.ExecuteScalar();

            if (id == null)
                return 0;

            return Convert.ToInt32(id);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return 0;
        }
    }
    public static List<Category> GetCategory()
    {

        List<Category> categories = new List<Category>();
        try
        {
            using var conn = GetConnection();
            using var cmd = new SqlCommand("SELECT Category_id, Category_title FROM Category Order by Category_id", conn);

            conn.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string title = reader.GetString(1);
                categories.Add(new Category(id, title));
            }

            return categories;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return categories;

        }
    }


    public static List<Item> GetItem()
    {
        List<Item> items = new List<Item>();
        try
        {
            using var conn = GetConnection();
            using var cmd = new SqlCommand("SELECT Item_id, Item_title, Item_price, Category_id FROM Item Order by Category_id", conn);

            conn.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                decimal price = reader.GetDecimal(2);
                int categoryId = reader.GetInt32(3);
                items.Add(new Item(id, name, price, categoryId));
            }

            return items;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return items;
        }
    }






    public static void InsertOrderWithItems(Order order, List<OrderItem> items)
    {
        try
        {
            using var conn = GetConnection();
            conn.Open();

            using var orderCmd = new SqlCommand(
              @"INSERT INTO Orders (Create_date, Employee_id, Total_price)
              VALUES (@date, @empId, @total);
             SELECT SCOPE_IDENTITY();", conn);

            orderCmd.Parameters.Add("@date", SqlDbType.DateTime).Value = order.OrderDate;
            orderCmd.Parameters.Add("@empId", SqlDbType.Int).Value = order.EmployeeId;
            orderCmd.Parameters.Add("@total", SqlDbType.Decimal).Value = order.TotalPrice;


            int orderId = Convert.ToInt32(orderCmd.ExecuteScalar());


            foreach (var item in items)
            {
                using var itemCmd = new SqlCommand(
                    @"INSERT INTO OrderItem
                    (OrderItem_price, OrderItem_quantity, Item_id, Orders_id)
                     VALUES (@price, @qty, @itemId, @orderId)", conn);

                itemCmd.Parameters.Add("@price", SqlDbType.Decimal).Value = item.Price;
                itemCmd.Parameters.Add("@qty", SqlDbType.Int).Value = item.Quantity;
                itemCmd.Parameters.Add("@itemId", SqlDbType.Int).Value = item.ItemId;
                itemCmd.Parameters.Add("@orderId", SqlDbType.Int).Value = orderId;

                itemCmd.ExecuteNonQuery();


            }


        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);

        }
    }

    public static List<Employee> GetEmployees()
    {
        List<Employee> employees = new List<Employee>();
        try
        {
            using var conn = GetConnection();
            using var cmd = new SqlCommand("SELECT Employee_id, Employee_name FROM Employee Order by Employee_id", conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                employees.Add(new Employee(id, name));
            }
            return employees;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return employees;
        }

    }

    public static List<Order> GetOrdersByDay(DateTime currentDateTime)
    {
        try
        {

            using var conn = GetConnection();

            DateTime start = currentDateTime.Date;

            DateTime end = start.AddDays(1);


            using var cmd = new SqlCommand(@"
    select 
        o.Orders_id,
        o.Total_price,
        o.Create_date,
        e.Employee_id,
        e.Employee_name,
        i.Item_id,
        i.Item_title,
        oi.OrderItem_quantity,
        oi.OrderItem_price
    from Orders o
    join Employee e on o.Employee_id = e.Employee_id
    left join OrderItem oi on o.Orders_id = oi.Orders_id
    left join Item i on oi.Item_id = i.Item_id
    where o.Create_date >= @start and o.Create_date < @end
    order by o.Orders_id desc;", conn);


            cmd.Parameters.AddWithValue("@start", start);
            cmd.Parameters.AddWithValue("@end", end);

            conn.Open();

            var ordersDict = new Dictionary<int, Order>();

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
            
                int orderId = reader.GetInt32(0);

                if (!ordersDict.ContainsKey(orderId))
                {
                    var order = new Order
                    {
                        Id = orderId,
                        TotalPrice = reader.GetDecimal(1),
                        OrderDate = reader.GetDateTime(2),
                        EmployeeId = reader.GetInt32(3),
                        EmployeeName = reader.GetString(4),
                        Items = new List<OrderItem>()
                    };

                    ordersDict.Add(orderId, order);
                }

        
                if (!reader.IsDBNull(5))
                {
                    var orderItem = new OrderItem
                    {
                        ItemId = reader.GetInt32(5),
                        ItemName = reader.GetString(6),
                        Quantity = reader.GetInt32(7),
                        Price = reader.GetDecimal(8)
                    };

                    ordersDict[orderId].Items.Add(orderItem);
                }
            }


            return ordersDict.Values.ToList();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return new List<Order>();
        }




    }
}
