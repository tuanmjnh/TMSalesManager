using System;
using System.Web;
namespace TM
{
    public class ResizeImage
    {
        public static System.Drawing.Image RezizeImage(System.Drawing.Image img, int maxWidth, int maxHeight)
        {
            if (img.Height < maxHeight && img.Width < maxWidth) return img;
            using (img)
            {
                Double xRatio = (double)img.Width / maxWidth;
                Double yRatio = (double)img.Height / maxHeight;
                Double ratio = Math.Max(xRatio, yRatio);
                int nnx = (int)Math.Floor(img.Width / ratio);
                int nny = (int)Math.Floor(img.Height / ratio);
                System.Drawing.Bitmap cpy = new System.Drawing.Bitmap(nnx, nny, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                using (System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(cpy))
                {
                    gr.Clear(System.Drawing.Color.Transparent);
                    // This is said to give best quality when resizing images
                    gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    gr.DrawImage(img,
                        new System.Drawing.Rectangle(0, 0, nnx, nny),
                        new System.Drawing.Rectangle(0, 0, img.Width, img.Height),
                        System.Drawing.GraphicsUnit.Pixel);
                }
                return cpy;
            }
        }
        public static System.IO.MemoryStream ByteArrayToStream(byte[] arr)
        {
            return new System.IO.MemoryStream(arr, 0, arr.Length);
        }
    }
}