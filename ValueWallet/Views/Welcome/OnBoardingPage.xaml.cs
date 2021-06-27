using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Forms;

namespace ValueWallet.Views.Welcome
{
    public partial class OnBoardingPage : ContentPage
    {
        public OnBoardingPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            Task.Run( async () =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {

                    FingerprintAuthenticationResult authResult = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Heads up!", "I would like to use your biometrics, please!"));

                    if (authResult.Authenticated)
                    {
                        await DisplayAlert("Yaay!", "Here is the secrets", "Thanks!");
                    }

                });
                   
            });

        }
    }
}
