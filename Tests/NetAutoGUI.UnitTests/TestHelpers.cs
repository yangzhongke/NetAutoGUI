using System.Drawing;
using NetAutoGUI.Windows;

namespace NetAutoGUI.UnitTests;

public static class TestHelpers
{
   public static BitmapData LoadBitmapDataFromBmpFile(string path)
   {
      using var bitmap = (Bitmap)Image.FromFile(path);
      //Act
      return bitmap.ToBitmapData();
   }
}