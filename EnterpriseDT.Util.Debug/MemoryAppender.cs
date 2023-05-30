using System;
using System.Collections;
using System.IO;

namespace EnterpriseDT.Util.Debug;

public class MemoryAppender : Appender
{
	public const int DEFAULT_MAX_MESSAGES = 1000;

	private int maxMessages = 1000;

	private ArrayList messages = new ArrayList();

	public int MaxMessages
	{
		get
		{
			return maxMessages;
		}
		set
		{
			lock (messages.SyncRoot)
			{
				maxMessages = value;
			}
		}
	}

	public string[] Messages => (string[])messages.ToArray(typeof(string));

	public MemoryAppender()
	{
	}

	public MemoryAppender(int maxMessages)
	{
		this.maxMessages = maxMessages;
	}

	public virtual void Log(string msg)
	{
		AddMessage(msg);
	}

	public virtual void Log(Exception t)
	{
		AddMessage(((object)t).GetType().FullName + ": " + t.Message);
		AddMessage(t.StackTrace.ToString());
	}

	private void AddMessage(string msg)
	{
		lock (messages.SyncRoot)
		{
			if (messages.Count == maxMessages)
			{
				messages.RemoveAt(0);
			}
			messages.Add(msg);
		}
	}

	public virtual void Close()
	{
	}

	public void Write(string path)
	{
		using StreamWriter stream = File.CreateText(path);
		Write(stream);
	}

	public void Write(StreamWriter stream)
	{
		string[] array = Messages;
		foreach (string value in array)
		{
			stream.WriteLine(value);
		}
	}
}
