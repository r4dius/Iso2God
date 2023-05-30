using System;
using System.IO;

namespace EnterpriseDT.Util.Debug;

public class FileAppender : Appender
{
	protected TextWriter logger;

	protected FileStream fileStream;

	private string fileName;

	protected bool closed = false;

	public string FileName => fileName;

	public FileAppender(string fileName)
	{
		this.fileName = fileName;
		Open();
	}

	protected void Open()
	{
		fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
		logger = TextWriter.Synchronized(new StreamWriter(fileStream));
		closed = false;
	}

	public virtual void Log(string msg)
	{
		if (!closed)
		{
			logger.WriteLine(msg);
			logger.Flush();
		}
		else
		{
			Console.WriteLine(msg);
		}
	}

	public virtual void Log(Exception t)
	{
		if (!closed)
		{
			logger.WriteLine(((object)t).GetType().FullName + ": " + t.Message);
			logger.WriteLine(t.StackTrace.ToString());
			logger.Flush();
		}
		else
		{
			Console.WriteLine(((object)t).GetType().FullName + ": " + t.Message);
		}
	}

	public virtual void Close()
	{
		logger.Flush();
		logger.Close();
		logger = null;
		fileStream = null;
		closed = true;
	}
}
