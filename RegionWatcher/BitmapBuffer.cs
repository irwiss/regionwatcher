using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace RegionWatcher
{
    public class BitmapBuffer : IDisposable
    {
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;

        public BitmapBuffer(int width, int height)
        {
            bitmap = new(width, height);
            graphics = Graphics.FromImage(bitmap);
        }

        public void CopyFromScreen(Point upperLeftSource)
        {
            graphics.CopyFromScreen(upperLeftSource, Point.Empty, bitmap.Size);
        }

        public void Dispose()
        {
            graphics.Dispose();
            bitmap.Dispose();
            GC.SuppressFinalize(this);
        }

        public bool HasSameDataAs(BitmapBuffer? other)
        {
            if (other == null || this.bitmap.Size != other.bitmap.Size)
            {
                return false;
            }

            Rectangle rect = new(Point.Empty, this.bitmap.Size);

            BitmapData bitmapData = this.bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData bitmapData2 = other.bitmap.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            try
            {
                return NativeMethods.MemCmp(bitmapData.Scan0, bitmapData2.Scan0, (UIntPtr)(ulong)(bitmapData.Stride * this.bitmap.Height)) == 0;
            }
            finally
            {
                this.bitmap.UnlockBits(bitmapData);
                other.bitmap.UnlockBits(bitmapData2);
            }
        }
    }
}
