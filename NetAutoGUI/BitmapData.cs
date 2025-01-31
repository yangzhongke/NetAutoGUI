using System;
using System.IO;
using SkiaSharp;

namespace NetAutoGUI;

/// <summary>
///     Managed Bitmap, there is no need to dispose it explicitly.
/// </summary>
/// <param name="Data">bitmap format data of an image</param>
/// <param name="Width">Width of the image</param>
/// <param name="Height">Heigh of the image</param>
public record BitmapData(byte[] Data, int Width, int Height)
{
    public static Func<string, BitmapData> LoadFromFileFunc { get; set; }

    public static BitmapData FromFile(string imageFile)
    {
        if (LoadFromFileFunc == null) throw new InvalidOperationException("LoadFromFileFunc is not set");
        return LoadFromFileFunc(imageFile);
    }

    private static void CreateDir(string filename)
    {
        new FileInfo(filename).Directory!.Create();
    }

    /// <summary>
    ///     Save the image into a local file.
    /// </summary>
    /// <param name="filename">file name</param>
    /// <param name="imgType">saved image format</param>
    public void Save(string filename, ImageType? imgType = null)
    {
        CreateDir(filename);
        using var output = File.OpenWrite(filename);
        if (imgType == null) imgType = InferImageType(filename);
        Save(output, imgType.Value);
    }

    /// <summary>
    ///     Save the image into a stream
    /// </summary>
    /// <param name="outStream">the output stream</param>
    /// <param name="imgType">saved image format</param>
    public void Save(Stream outStream, ImageType imgType)
    {
        using var image = SKImage.FromEncodedData(Data);
        using var encodedImg = image.Encode(ToImageFormat(imgType), 100);
        encodedImg.SaveTo(outStream);
    }

    private static ImageType InferImageType(string filename)
    {
        var ext = Path.GetExtension(filename);
        if (ext == null) throw new ArgumentOutOfRangeException(nameof(filename), "no file extension");
        ext = ext.ToLower();
        switch (ext)
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
        switch (imgType)
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