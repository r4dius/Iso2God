using System;

namespace EnterpriseDT.Net.Ftp;

public class MethodIdentifierAttribute : Attribute
{
	private MethodIdentifier identifier;

	private Type[] argumentTypes = null;

	public MethodIdentifier Identifier => identifier;

	public Type[] ArgumentTypes => argumentTypes;

	public MethodIdentifierAttribute(MethodIdentifier identifier)
	{
		this.identifier = identifier;
		argumentTypes = new Type[0];
	}

	public MethodIdentifierAttribute(MethodIdentifier identifier, Type arg1)
		: this(identifier, arg1, null, null, null)
	{
		this.identifier = identifier;
		argumentTypes = new Type[1];
		argumentTypes[0] = arg1;
	}

	public MethodIdentifierAttribute(MethodIdentifier identifier, Type arg1, Type arg2)
		: this(identifier, arg1, arg2, null, null)
	{
		this.identifier = identifier;
		argumentTypes = new Type[2];
		argumentTypes[0] = arg1;
		argumentTypes[1] = arg2;
	}

	public MethodIdentifierAttribute(MethodIdentifier identifier, Type arg1, Type arg2, Type arg3)
		: this(identifier, arg1, arg2, arg3, null)
	{
		this.identifier = identifier;
		argumentTypes = new Type[3];
		argumentTypes[0] = arg1;
		argumentTypes[1] = arg2;
		argumentTypes[2] = arg3;
	}

	public MethodIdentifierAttribute(MethodIdentifier identifier, Type arg1, Type arg2, Type arg3, Type arg4)
	{
		this.identifier = identifier;
		argumentTypes = new Type[4];
		argumentTypes[0] = arg1;
		argumentTypes[1] = arg2;
		argumentTypes[2] = arg3;
		argumentTypes[3] = arg4;
	}
}
