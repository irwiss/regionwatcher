using System.Drawing;

namespace RegionWatcher
{
    public static class Helpers
    {
        public static Point Int32ToPoint(int i) => new(i & 65535, i >> 16);

        public static int PointToInt32(Point p) => p.X + (p.Y << 16);

        public static Size Int32ToSize(int i) => new(i & 65535, i >> 16);

        public static int SizeToInt32(Size s) => s.Width + (s.Height << 16);
    }
}
