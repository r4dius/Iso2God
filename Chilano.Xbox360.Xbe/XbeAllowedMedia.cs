namespace Chilano.Xbox360.Xbe;

public enum XbeAllowedMedia : uint
{
    HardDisk = 1u,
    DvdX2 = 2u,
    DvdCd = 4u,
    Cd = 8u,
    Dvd5Ro = 16u,
    Dvd9Ro = 32u,
    Dvd5Rw = 64u,
    Dvd9Rw = 128u,
    Dongle = 256u,
    MediaBoard = 512u,
    NonSecureHD = 1073741824u,
    NonSecureMode = 2147483648u,
    MediaMask = 16777215u
}
