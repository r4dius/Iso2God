using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Chilano.Iso2God.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
[CompilerGenerated]
[DebuggerNonUserCode]
internal class Resources
{
	private static ResourceManager resourceMan;

	private static CultureInfo resourceCulture;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager
	{
		get
		{
			if (object.ReferenceEquals(resourceMan, null))
			{
				ResourceManager resourceManager = new ResourceManager("Chilano.Iso2God.Properties.Resources", typeof(Resources).Assembly);
				resourceMan = resourceManager;
			}
			return resourceMan;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static CultureInfo Culture
	{
		get
		{
			return resourceCulture;
		}
		set
		{
			resourceCulture = value;
		}
	}

	internal static Icon AppIcon
	{
		get
		{
			object @object = ResourceManager.GetObject("AppIcon", resourceCulture);
			return (Icon)@object;
		}
	}

	internal static Bitmap Application
	{
		get
		{
			object @object = ResourceManager.GetObject("Application", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap Create
	{
		get
		{
			object @object = ResourceManager.GetObject("Create", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static byte[] emptyLIVE
	{
		get
		{
			object @object = ResourceManager.GetObject("emptyLIVE", resourceCulture);
			return (byte[])@object;
		}
	}

	internal static Bitmap Go
	{
		get
		{
			object @object = ResourceManager.GetObject("Go", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap Hint
	{
		get
		{
			object @object = ResourceManager.GetObject("Hint", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap Info
	{
		get
		{
			object @object = ResourceManager.GetObject("Info", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap LogoAbout
	{
		get
		{
			object @object = ResourceManager.GetObject("LogoAbout", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap LogoToolbar
	{
		get
		{
			object @object = ResourceManager.GetObject("LogoToolbar", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap No_entry
	{
		get
		{
			object @object = ResourceManager.GetObject("No_entry", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap ToolbarBg
	{
		get
		{
			object @object = ResourceManager.GetObject("ToolbarBg", resourceCulture);
			return (Bitmap)@object;
		}
	}

	internal Resources()
	{
	}
}
