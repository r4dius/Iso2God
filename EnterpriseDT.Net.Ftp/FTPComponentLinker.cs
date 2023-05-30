using System;
using System.ComponentModel;

namespace EnterpriseDT.Net.Ftp;

internal class FTPComponentLinker
{
	public static void Link(ISite site, IFTPComponent component)
	{
		if (site == null || !site.DesignMode || site.Container == null)
		{
			return;
		}
		foreach (object component2 in site.Container.Components)
		{
			if (component2 != component && component2 is IFTPComponent)
			{
				((IFTPComponent)component2).LinkComponent(component);
			}
		}
	}

	public static Component Find(ISite site, Type componentType)
	{
		if (site == null || site.Container == null)
		{
			return null;
		}
		foreach (object component in site.Container.Components)
		{
			if (componentType.IsInstanceOfType(component))
			{
				return (Component)component;
			}
		}
		return null;
	}
}
