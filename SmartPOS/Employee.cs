using System;

public class Employee
{
	public int Id { get; set; }
	public string Name { get; set; }

    public Employee(int Id, string Name)
	{
		this.Id = Id;
		this.Name = Name;
    }

	public override string ToString()
	{
		return this.Name;
    }
}
