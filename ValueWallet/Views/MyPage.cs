using System;

using Xamarin.Forms;

namespace ValueWallet.Views
{
    public class MyPage : ContentPage
    {
        public MyPage()
        {

            var b = new StackLayout
            {

            };

            var a = new Label
            {
                Text = "Hello ContentPage",
                Padding = new Thickness(0),
                BackgroundColor = Color.Red
            };

            b.Children.Add(a);

            Content = b;
        }
    }
}

