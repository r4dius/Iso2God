using System;
using System.ComponentModel;

namespace Chilano.Iso2God;

public class Iso2GodProgressArgs : EventArgs
{
	public int Percentage;

	public string Message;

	public Iso2GodProgressArgs(ProgressChangedEventArgs e)
	{
		Percentage = e.ProgressPercentage;
		Message = e.UserState.ToString();
	}
}
