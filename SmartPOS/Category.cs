using System;

public class Category
{
	public int Id { get; set; }
	public string Title { get; set; }

    public Category(int Id, string Title)
	{
		this.Id = Id;
		this.Title = Title;
    }
}
