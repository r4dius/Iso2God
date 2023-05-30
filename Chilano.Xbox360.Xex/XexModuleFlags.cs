namespace Chilano.Xbox360.Xex;

public class XexModuleFlags : XexInfoField
{
    private enum ModuleFlags : uint
    {
        TitleModule = 1u,
        ExportsToTitle = 2u,
        SystemDebugger = 4u,
        DllModule = 8u,
        ModulePatch = 0x10u,
        FullPatch = 0x20u,
        DeltaPatch = 0x40u,
        UserMode = 0x80u
    }

    public static byte[] Signature = new byte[4] { 0, 3, 0, 0 };

    public bool TitleModule => (Address & 1) == 1;

    public bool ExportsToTitle => (Address & 2) == 2;

    public bool SystemDebugger => (Address & 4) == 4;

    public bool DllModule => (Address & 8) == 8;

    public bool ModulePatch => (Address & 0x10) == 16;

    public bool FullPatch => (Address & 1) == 32;

    public bool DeltaPatch => (Address & 1) == 64;

    public bool UserMode => (Address & 1) == 128;

    public XexModuleFlags(uint Address)
        : base(Address)
    {
        base.Flags = true;
    }
}
