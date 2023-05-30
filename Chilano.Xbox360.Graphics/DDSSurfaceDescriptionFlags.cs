namespace Chilano.Xbox360.Graphics;

public enum DDSSurfaceDescriptionFlags : uint
{
    DDSD_CAPS = 1u,
    DDSD_HEIGHT = 2u,
    DDSD_WIDTH = 4u,
    DDSD_PITCH = 8u,
    DDSD_PIXELFORMAT = 0x1000u,
    DDSD_MIPMAPCOUNT = 0x20000u,
    DDSD_LINEARSIZE = 0x80000u,
    DDSD_DEPTH = 0x800000u
}
