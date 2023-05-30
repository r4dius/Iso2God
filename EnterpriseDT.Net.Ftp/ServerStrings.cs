using System.Collections;
using System.ComponentModel;

namespace EnterpriseDT.Net.Ftp;

public class ServerStrings : CollectionBase
{
	private PropertyChangedEventHandler propertyChangeHandler;

	public string this[int index]
	{
		get
		{
			return (string)base.List[index];
		}
		set
		{
			if ((string)base.List[index] != value)
			{
				base.List[index] = value;
				OnMemberChanged();
			}
		}
	}

	internal PropertyChangedEventHandler PropertyChangeHandler
	{
		get
		{
			return propertyChangeHandler;
		}
		set
		{
			propertyChangeHandler = value;
		}
	}

	public void Add(string str)
	{
		base.List.Add(str);
		OnMemberChanged();
	}

	public void AddRange(string[] strs)
	{
		foreach (string value in strs)
		{
			base.List.Add(value);
		}
		OnMemberChanged();
	}

	public bool Contains(string str)
	{
		return base.List.Contains(str);
	}

	public void CopyTo(string[] array, int index)
	{
		base.List.CopyTo(array, index);
	}

	public int IndexOf(string str)
	{
		return base.List.IndexOf(str);
	}

	public void Insert(int index, string str)
	{
		base.List.Insert(index, str);
		OnMemberChanged();
	}

	public void Remove(string str)
	{
		base.List.Remove(str);
		OnMemberChanged();
	}

	public bool Matches(string reply)
	{
		string text = reply.ToUpper();
		for (int i = 0; i < base.Count; i++)
		{
			string text2 = this[i];
			if (text.IndexOf(text2.ToUpper()) >= 0)
			{
				return true;
			}
		}
		return false;
	}

	private void OnMemberChanged()
	{
		if (propertyChangeHandler != null)
		{
			propertyChangeHandler(this, new PropertyChangedEventArgs(null));
		}
	}
}
