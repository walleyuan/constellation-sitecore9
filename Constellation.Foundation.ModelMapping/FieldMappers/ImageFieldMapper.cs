﻿using Constellation.Foundation.ModelMapping.MappingAttributes;
using Sitecore.Data.Fields;
using Sitecore.Resources.Media;
using System;
using System.Reflection;

namespace Constellation.Foundation.ModelMapping.FieldMappers
{
	public class ImageFieldMapper : FieldMapper<Uri>
	{
		protected override string ExtractStringValueFromField()
		{
			var urlAttribute = Property.GetCustomAttribute<RenderAsUrlAttribute>();

			if (urlAttribute == null || urlAttribute.UseFieldRendererInEditor && Sitecore.Context.PageMode.IsExperienceEditorEditing)
			{
				return base.ExtractStringValueFromField();
			}

			return GetUrlFromField();
		}

		protected override Uri ExtractTypedValueFromField()
		{
			var url = GetUrlFromField();

			if (!string.IsNullOrEmpty(url))
			{
				return new Uri(GetUrlFromField());
			}

			return null;
		}

		private string GetUrlFromField()
		{
			var targetImage = ((ImageField)Field).MediaItem;

			if (targetImage != null)
			{
				return MediaManager.GetMediaUrl(targetImage);
			}

			return string.Empty;
		}
	}
}