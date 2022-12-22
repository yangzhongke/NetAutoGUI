using OpenCvSharp;
using SkiaSharp;
using System;
using System.IO;

namespace NetAutoGUI
{
    public record BitmapData(byte[] Data,int Width,int Height)
    {
        public Mat ToMat()
        {
            return Cv2.ImDecode(Data,ImreadModes.Unchanged);
        }

        public void Save(string filename, ImageType imgType)
        {
            using var image = SKImage.FromEncodedData(Data);
            using var output =File.OpenWrite(filename);
            using var encodedImg = image.Encode(ToImageFormat(imgType), 100);
            encodedImg.SaveTo(output);
        }

        public void Save(Stream outStream, ImageType imgType)
        {
            using var image = SKImage.FromEncodedData(Data);
            using var encodedImg = image.Encode(ToImageFormat(imgType), 100);
            encodedImg.SaveTo(outStream);
        }

        public static BitmapData Load(Stream inStream)
        {
            using var image = SKBitmap.Decode(inStream);
            using var encodedImg = image.Encode(SKEncodedImageFormat.Bmp, 100);
            byte[] data = encodedImg.ToArray();
            return new BitmapData(data, image.Width, image.Height);
        }

        public static BitmapData Load(string filename)
        {
            using var inStream = File.OpenRead(filename);
            return Load(inStream);
        }

        private static SKEncodedImageFormat ToImageFormat(ImageType imgType)
        {
            switch(imgType)
            {
                case ImageType.Bmp:
                    return SKEncodedImageFormat.Bmp;
                case ImageType.Jpg:
                    return SKEncodedImageFormat.Jpeg;
                case ImageType.Png:
                    return SKEncodedImageFormat.Png;
                default:
                    throw new ArgumentOutOfRangeException(nameof(imgType));
            }
        }
    }
}
