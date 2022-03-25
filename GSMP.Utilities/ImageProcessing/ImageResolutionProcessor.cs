using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Metadata;
using SixLabors.ImageSharp.Processing;

namespace GSMP.Utilities.ImageProcessing
{
	public class ImageResolutionProcessor
	{
		private readonly Size   _thumbnailSize;
		private readonly Size   _normalSize;
		private readonly double _newDpi;

		private Configuration _imageSharpConfiguration;

		public ImageResolutionProcessor(ImageResolutionProcessorOptions options = null)
		{
			var o = options ?? ImageResolutionProcessorOptions.Default;

			_thumbnailSize = new Size(o.ThumbnailSize);
			_normalSize    = new Size(o.NormalSize);
			_newDpi        = o.NewDpi;

			_imageSharpConfiguration = Configuration.Default;
		}

		public bool IsFileNameSupported(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
				return false;

			var extension = Path.GetExtension(fileName);
			if (string.IsNullOrEmpty(extension))
				return false;

			extension = extension.Substring(1);

			return GetSupportedFileExtensions()
				.Any(x => string.Equals(extension, x, StringComparison.OrdinalIgnoreCase));
		}

		public IEnumerable<string> GetSupportedFileExtensions()
		{
			return _imageSharpConfiguration.ImageFormats.SelectMany(x => x.FileExtensions);
		}

		public IEnumerable<string> GetSupportedMimeTypes()
		{
			return _imageSharpConfiguration.ImageFormats.SelectMany(x => x.MimeTypes);
		}

		public bool IsImageSupported(Stream stream)
		{
			var format = Image.DetectFormat(_imageSharpConfiguration, stream);
			return format != null;
		}

		public async Task<bool> IsImageSupportedAsync(Stream stream)
		{
			var format = await Image.DetectFormatAsync(_imageSharpConfiguration, stream)
				.ConfigureAwait(false);

			return format != null;
		}

		public IEnumerable<ImageData> MultiplyResolutions(string fileName, Stream stream)
		{
			return MultiplyResolutionsAsync(fileName, stream)
				.ConfigureAwait(false)
				.GetAwaiter()
				.GetResult();
		}

		public async Task<IEnumerable<ImageData>> MultiplyResolutionsAsync(string fileName, Stream stream, CancellationToken cancellationToken = default)
		{
			if (stream.CanSeek)
				stream.Position = 0;

			var isSupported = await IsImageSupportedAsync(stream)
				.ConfigureAwait(false);

			if (!isSupported)
			{
				//unsupported file format
				return new[]
				{
					new ImageData
					{
						FileName  = fileName
						, Content = stream
					}
				};
			}

			var cache = new MemoryStream();
			await stream.CopyToAsync(cache, (int)stream.Length, cancellationToken)
				.ConfigureAwait(false);

			cache.Position = 0;

			var result = new List<ImageData>
			{
				CreateOriginal(fileName, cache)
			};

			var image = await Image.LoadAsync(_imageSharpConfiguration, cache, cancellationToken)
				.ConfigureAwait(false);
			using (image)
			{
				result.Add(CreateImageData(fileName, cache, image, _normalSize,    ImageResolutionProcessorOptions.NormalSuffix));
				result.Add(CreateImageData(fileName, cache, image, _thumbnailSize, ImageResolutionProcessorOptions.ThumbnailSuffix));
			}
			cache.Position = 0;

			return result;
		}

		private static ImageData CreateOriginal(string fileName, Stream image)
		{
			return new ImageData
			{
				Content    = image
				, FileName = fileName + ImageResolutionProcessorOptions.OriginalSuffix
			};
		}

		private ImageData CreateImageData(string fileName, Stream originalStream, Image image, Size newSize, string fileNameSuffix)
		{
			var content = CreateResizedImage(image, newSize, _newDpi);
			if (content == null)
			{
				content = new MemoryStream();

				originalStream.Position = 0;
				originalStream.CopyTo(content);
				originalStream.Position = 0;

				content.Position = 0;
			}

			return new ImageData
			{
				Content    = content
				, FileName = fileName + fileNameSuffix
			};
		}

		private static Stream CreateResizedImage(Image image, Size newSize, double newDpi)
		{
			var biggerThanNewSize = image.Width > newSize.Width
				|| image.Height > newSize.Height;
			if(!biggerThanNewSize)
				return null;

			var resizedImage = image.Clone(context =>
			{
				var options = new ResizeOptions
				{
					Mode              = ResizeMode.Max
					, TargetRectangle = new Rectangle(new Point(0), newSize)
					, Size            = newSize
				};
				context.Resize(options);
			});

			resizedImage.Metadata.ResolutionUnits      = PixelResolutionUnit.PixelsPerInch;
			resizedImage.Metadata.HorizontalResolution = newDpi;
			resizedImage.Metadata.VerticalResolution   = newDpi;

			var encoder = new JpegEncoder
			{
				Subsample = JpegSubsample.Ratio420
			};

			var resizedImageStream = new MemoryStream();

			resizedImage.Save(resizedImageStream, encoder);

			resizedImageStream.Position = 0;

			return resizedImageStream;
		}
	}
}
