using System;
using System.Collections.Generic;
using ValueWallet.Models;
using Xamarin.Forms;

namespace ValueWallet.Views.ComponentsView
{
    public partial class CircleImgHead_CV : ContentView
    {
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly BindableProperty TextProperty =
                            BindableProperty.Create(
                        propertyName:nameof(Text),
                        returnType: typeof(string),
                        declaringType: typeof(CircleImgHead_CV),
                        defaultBindingMode: BindingMode.OneWay);

        public CircleImgHead_CV()
        {
            InitializeComponent();

            if (LocalDeviceInfo.CurrentDevice.IsNotchIosDevice)
                NotchBx.IsVisible = true;
            else
                NotchBx.IsVisible = false;

            BackgroundColor = (Color)Application.Current.Resources["BlueBackground"];

            BindingContext = this;
        }
    }
}
