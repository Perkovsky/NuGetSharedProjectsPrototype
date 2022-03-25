using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using ZXing;

namespace GSMP.Utilities.ImageProcessing.QR
{
	public static class BarcodeWriterGenericExtensions
	{
		public static Image<TPixel> WriteAsImageSharp<TPixel>(this IBarcodeWriterGeneric writer, string content)
			where TPixel : unmanaged, IPixel<TPixel>
		{
			var matrix  = writer.Encode(content);
			var options = writer.Options;

			int    width    = matrix.Width;
			int    height   = matrix.Height;
			Rgba32 rgba32_1 = new Rgba32(4278190080U);
			Rgba32 rgba32_2 = new Rgba32(uint.MaxValue);

			int num = 1;
			if (options != null)
			{
				if (options.Width > width)
					width = options.Width;

				if (options.Height > height)
					height = options.Height;

				num = width / matrix.Width;
				if (num > height / matrix.Height)
					num = height / matrix.Height;
			}

			Image<TPixel> image = new Image<TPixel>(matrix.Width, matrix.Height);
			for (int index1 = 0; index1 < matrix.Height; ++index1)
			{
				for (int index2 = 0; index2 < num; ++index2)
				{
					int index3 = num * index1 + index2;
					for (int index4 = 0; index4 < matrix.Width; ++index4)
					{
						Rgba32 rgba32_3 = matrix[index4, index1] ? rgba32_1 : rgba32_2;
						for (int index5 = 0; index5 < num; ++index5)
						{
							TPixel pixel = new TPixel();
							pixel.FromRgba32(rgba32_3);
							image[num * index4 + index5, index3] = pixel;
						}
					}
					for (int index4 = num * matrix.Width; index4 < width; ++index4)
					{
						TPixel pixel = new TPixel();
						pixel.FromRgba32(rgba32_2);
						image[index4, index3] = pixel;
					}
				}
			}
			return image;
		}
	}
}
