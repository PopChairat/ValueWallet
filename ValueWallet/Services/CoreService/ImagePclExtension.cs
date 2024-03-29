﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ValueWallet
{
    [ContentProperty("Source")]
    public class ImagePclExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null) return null;
            ImageSource imageSource = ImageSource.FromResource(Source);
            return imageSource;
        }
    }
}
