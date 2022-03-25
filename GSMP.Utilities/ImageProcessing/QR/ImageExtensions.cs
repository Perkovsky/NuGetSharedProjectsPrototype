using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using ZXing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace GSMP.Utilities.ImageProcessing.QR
{
	public static class ImageExtensions
	{
		public static void DrawQrCode<TColor>(this Image<TColor> image, string text, Rectangle rectangle, int qrVersion = 1, ErrorCorrectionLevel errorCorrection = null)
			where TColor : unmanaged, IPixel<TColor>
		{
			if(string.IsNullOrEmpty(text))
				return;

			errorCorrection ??= ErrorCorrectionLevel.H;

			var barcodeWriterOptions = new QrCodeEncodingOptions
			{
				Height          = rectangle.Height,
				Width           = rectangle.Width,
				PureBarcode     = true,
				Margin          = 0,
				QrVersion       = qrVersion,
				ErrorCorrection = errorCorrection,
			};
			var barcodeWriter = new BarcodeWriter<QRCodeWriter>
			{
				Options = barcodeWriterOptions,
				Format  = BarcodeFormat.QR_CODE,
			};

			const float visible = 1f;
			var qr = barcodeWriter.WriteAsImageSharp<Rgba32>(text);
			using (qr)
			{
				image.Mutate(x => x.DrawImage(qr, rectangle.Location, PixelColorBlendingMode.Normal, PixelAlphaCompositionMode.SrcOver, visible));
			}
		}
	}
}