namespace Chilano.Xbox360.Xbe;

public enum XbeSectionFlags : uint
{
    Writable = 1u,
    Preload = 2u,
    Executable = 4u,
    InsertedFile = 8u,
    HeadPageReadOnly = 0x10u,
    TailPageReadOnly = 0x20u
}
