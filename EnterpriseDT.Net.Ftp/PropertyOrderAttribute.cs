using System;

namespace EnterpriseDT.Net.Ftp;

public class PropertyOrderAttribute : Attribute
{
	private int order;

	public int Order
	{
		get
		{
			return order;
		}
		set
		{
			order = value;
		}
	}

	public PropertyOrderAttribute(int order)
	{
		this.order = order;
	}
}
