using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Chilano.Xbox360.IO;

namespace Chilano.Xbox360.Xex;

public class XexHeader : Dictionary<XexInfoFields, XexInfoField>
{
	public XexHeader(CBinaryReader br)
	{
		Clear();
		try
		{
			br.Seek(0L, SeekOrigin.Begin);
			string @string = Encoding.ASCII.GetString(br.ReadBytes(4));
			if (@string != "XEX2")
			{
				return;
			}
			br.Seek(4L, SeekOrigin.Begin);
			Add(XexInfoFields.ModuleFlags, new XexModuleFlags(br.ReadUInt32()));
			br.Seek(8L, SeekOrigin.Begin);
			Add(XexInfoFields.CodeOffset, new XexCodeOffset(br.ReadUInt32()));
			br.Seek(16L, SeekOrigin.Begin);
			Add(XexInfoFields.CertifcateOffset, new XexCertifcateOffset(br.ReadUInt32()));
			br.Seek(20L, SeekOrigin.Begin);
			uint num = br.ReadUInt32();
			for (int i = 0; i < num; i++)
			{
				byte[] value = br.ReadBytes(4);
				uint num2 = BitConverter.ToUInt32(value, 0);
				if (num2 == BitConverter.ToUInt32(XexResourceInfo.Signature, 0))
				{
					Add(XexInfoFields.ResourceInfo, new XexResourceInfo(br.ReadUInt32()));
				}
				else if (num2 == BitConverter.ToUInt32(XexCompressionInfo.Signature, 0))
				{
					Add(XexInfoFields.CompressionInfo, new XexCompressionInfo(br.ReadUInt32()));
				}
				else if (num2 == BitConverter.ToUInt32(XexExecutionInfo.Signature, 0))
				{
					Add(XexInfoFields.ExecutionInfo, new XexExecutionInfo(br.ReadUInt32()));
				}
				else if (num2 == BitConverter.ToUInt32(XexBaseFileFormat.Signature, 0))
				{
					Add(XexInfoFields.BaseFileFormat, new XexBaseFileFormat(br.ReadUInt32()));
				}
				else if (num2 == BitConverter.ToUInt32(XexBaseFileTimestamp.Signature, 0))
				{
					Add(XexInfoFields.BaseFileTimestamp, new XexBaseFileTimestamp(br.ReadUInt32()));
				}
				else if (num2 == BitConverter.ToUInt32(XexOriginalName.Signature, 0))
				{
					Add(XexInfoFields.OriginalName, new XexOriginalName(br.ReadUInt32()));
				}
				else if (num2 == BitConverter.ToUInt32(XexRatingsInfo.Signature, 0))
				{
					Add(XexInfoFields.RatingsInfo, new XexRatingsInfo(br.ReadUInt32()));
				}
				else if (num2 == BitConverter.ToUInt32(XexModuleFlags.Signature, 0))
				{
					Add(XexInfoFields.SystemFlags, new XexModuleFlags(br.ReadUInt32()));
				}
				else
				{
					br.ReadUInt32();
				}
			}
		}
		catch (EndOfStreamException)
		{
			Console.WriteLine("EndOfStreamException when trying to read XEX file header.");
		}
	}
}
