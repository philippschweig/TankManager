using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Resources;
using EFSresources.Library;

namespace EFSresources.EFSextensions
{
	public static class ImageExtensions
	{
		/// <summary>
		/// Resizes the pic.
		/// </summary>
		/// <param name="sourceImage">The source image.</param>
		/// <param name="newWidth">The new width.</param>
		/// <param name="newHeight">The new height.</param>
		/// <returns></returns>
		public static BitmapImage Resize(this BitmapImage sourceImage, int newWidth, int newHeight)
		{
			if (sourceImage != null)
			{
				WriteableBitmap wBitmap = new WriteableBitmap(sourceImage);
				BitmapImage pic = new BitmapImage();

				using (MemoryStream ms = new MemoryStream())
				{
					wBitmap.SaveJpeg(ms, newWidth, newHeight, 0, 100);
					pic.SetSource(ms);
				}

				return pic;
			}

			return null;
		}

		public static BitmapImage LoadFromAppContent(Uri imageUri)
        {
			AppInfo.LoadContent(imageUri);
			return null;
        }
	}
}
