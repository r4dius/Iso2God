using System;
using Chilano.Xbox360.IO;

namespace Chilano.Xbox360.Xbe;

public class XbeHeader
{
	public bool IsValid;

	public byte[] DigitalSignature = new byte[256];

	public uint BaseAddress;

	public uint SizeOfHeaders;

	public uint SizeOfImage;

	public uint SizeOfImageHeader;

	public byte[] TimeDate = new byte[4];

	public uint CertificateAddress;

	public uint NumberOfSections;

	public uint SectionHeadersAddress;

	public XbeInitFlags InitialisationFlags;

	public uint EntryPoint;

	public uint TLSAddress;

	public uint PEStackCommit;

	public uint PEHeapReserve;

	public uint PEHeapCommit;

	public uint PEBaseAddress;

	public uint PESizeOfImage;

	public uint PEChecksum;

	public byte[] PETimeDate = new byte[4];

	public uint DebugPathnameAddress;

	public uint DebugFilenameAddress;

	public uint DebugUnicodeFilenameAddress;

	public uint KernelImageThunkAddress;

	public uint NonKernelImportDirectoryAddress;

	public uint NumberOfLibraryVersions;

	public uint LibraryVersionsAddress;

	public uint KernelLibraryVersionAddress;

	public uint XAPILibraryVersionAddress;

	public uint LogoBitmapAddress;

	public uint LogoBitmapSize;

	public XbeHeader(CBinaryReader br)
	{
		try
		{
			if (br.ReadUInt32() == 1212498520)
			{
				DigitalSignature = br.ReadBytes(256);
				BaseAddress = br.ReadUInt32();
				SizeOfHeaders = br.ReadUInt32();
				SizeOfImage = br.ReadUInt32();
				SizeOfImageHeader = br.ReadUInt32();
				TimeDate = br.ReadBytes(4);
				CertificateAddress = br.ReadUInt32();
				NumberOfSections = br.ReadUInt32();
				SectionHeadersAddress = br.ReadUInt32();
				InitialisationFlags = (XbeInitFlags)br.ReadUInt32();
				EntryPoint = br.ReadUInt32();
				TLSAddress = br.ReadUInt32();
				PEStackCommit = br.ReadUInt32();
				PEHeapReserve = br.ReadUInt32();
				PEHeapCommit = br.ReadUInt32();
				PEBaseAddress = br.ReadUInt32();
				PESizeOfImage = br.ReadUInt32();
				PEChecksum = br.ReadUInt32();
				PETimeDate = br.ReadBytes(4);
				DebugPathnameAddress = br.ReadUInt32();
				DebugFilenameAddress = br.ReadUInt32();
				DebugUnicodeFilenameAddress = br.ReadUInt32();
				KernelImageThunkAddress = br.ReadUInt32();
				NonKernelImportDirectoryAddress = br.ReadUInt32();
				NumberOfLibraryVersions = br.ReadUInt32();
				LibraryVersionsAddress = br.ReadUInt32();
				KernelLibraryVersionAddress = br.ReadUInt32();
				XAPILibraryVersionAddress = br.ReadUInt32();
				LogoBitmapAddress = br.ReadUInt32();
				LogoBitmapSize = br.ReadUInt32();
				IsValid = true;
			}
		}
		catch (Exception)
		{
		}
	}
}
