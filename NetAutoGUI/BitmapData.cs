using OpenCvSharp;
using SkiaSharp;
using System;
using System.IO;
using Zack.Commons;

namespace NetAutoGUI
{
    public record BitmapData(byte[] Data,int Width,int Height)
    {
        public Mat ToMat(ImreadModes flags = ImreadModes.Unchanged)
        {
            return Cv2.ImDecode(Data,flags);
        }

        private static void CreateDir(string filename)
        {
            IOHelper.CreateDir(new FileInfo(filename));
        }
        public void Save(string filename, ImageType? imgType=null)
        {
            CreateDir(filename);
            using var output =File.OpenWrite(filename);
            if(imgType==null)
            {
                imgType = InferImageType(filename);
            }
            Save(output,imgType.Value);
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

        private static ImageType InferImageType(string filename)
        {
            string? ext = Path.GetExtension(filename);
            if(ext==null)
            {
                throw new ArgumentOutOfRangeException(nameof(filename),"no file extension");
            }
            ext = ext.ToLower();
            switch(ext)
            {
                case ".jpg":
                case ".jpeg":
                    return ImageType.Jpg;
                case ".png":
                    return ImageType.Png;
                case ".webp":
                    return ImageType.WebP;
                default:
                    throw new ArgumentOutOfRangeException(nameof(filename), $"unexpected file extension:{ext}");
            }
        }

        private static SKEncodedImageFormat ToImageFormat(ImageType imgType)
        {
            switch(imgType)
            {
                case ImageType.WebP:
                    return SKEncodedImageFormat.Webp;
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
