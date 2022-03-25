using System;

namespace GSMP.Utilities.ImageProcessing
{
	public class ImageResolutionProcessorOptions
	{
		private static Lazy<ImageResolutionProcessorOptions> _default = new Lazy<ImageResolutionProcessorOptions>(() => new ImageResolutionProcessorOptions());
		public static  ImageResolutionProcessorOptions       Default => _default.Value;

		public const string NormalSuffix    = "";
		public const string ThumbnailSuffix = "_thumbnail";
		public const string OriginalSuffix  = "_original";

		private int _thumbnailSize;
		public int ThumbnailSize
		{
			get => _thumbnailSize;
			set
			{
				CheckNewSize(nameof(ThumbnailSize), value);
				_thumbnailSize = value;
			}
		}

		private int _normalSize;
		public int NormalSize
		{
			get => _normalSize;
			set
			{
				CheckNewSize(nameof(NormalSize), value);
				_normalSize = value;
			}
		}

		private double _newDpi;
		public double NewDpi
		{
			get => _newDpi;
			set
			{
				if (value <= 0d)
					throw OutOfRange(nameof(NewDpi), value);
				_newDpi = value;
			}
		}

		public ImageResolutionProcessorOptions()
		{
			ThumbnailSize = 150;
			NormalSize    = 960;
			NewDpi        = 96d;
		}

		private static void CheckNewSize(string propertyName, int newSize)
		{
			if (newSize < 1)
				throw OutOfRange(propertyName, newSize);
		}
		private static Exception OutOfRange(string propertyName, object actualValue)
		{
			return new ArgumentOutOfRangeException(propertyName, actualValue, "Only positive values are accepted");
		}
	}
}