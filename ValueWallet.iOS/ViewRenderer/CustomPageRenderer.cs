using System;
using UIKit;
using ValueWallet.iOS.ViewRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Page), typeof(CustomPageRenderer))]
namespace ValueWallet.iOS.ViewRenderer
{
    public class CustomPageRenderer : PageRenderer
    {
        /// <summary>
        /// Thank Logic detect Notch :
        /// https://stackoverflow.com/questions/47779937/how-to-allow-for-ios-status-bar-and-iphone-x-notch-in-xamarin-forms
        /// </summary>
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement is Page currentPage)
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
                {
                    //Disable dark mode
                    OverrideUserInterfaceStyle = UIUserInterfaceStyle.Light;
                }

                if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                {
                    UIEdgeInsets insets = UIApplication.SharedApplication.Windows[0].SafeAreaInsets;
                    Thickness currentThickness = currentPage.Padding;
                    nfloat margin = 20f;
                    if (insets.Top > 0)
                    {
                        // We have a notch
                        margin = 35f;
                    }

                    if (currentThickness.Top < margin)
                        currentPage.Padding = new Thickness(currentThickness.Left, margin, currentThickness.Right, currentThickness.Bottom);
                }
            }
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (UIDevice.CurrentDevice.CheckSystemVersion(12, 0))
            {
                ViewController.AdditionalSafeAreaInsets = new UIEdgeInsets(60, 0, 0, 0);
            }
        }
    }
}
