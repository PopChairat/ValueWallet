using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using ValueWallet.Services.CoreService;
using Xamarin.Forms;

namespace ValueWallet.Views.LoginNRegister
{
    public partial class LoginNewUserPage : ContentPage
    {
        public LoginNewUserPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
               
                FingerprintAuthenticationResult authResult = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Heads up!", "I would like to use your biometrics, please!"));

                if (authResult.Authenticated)
                {
                    string a = CryptoManagerApp.EncryptData(userEntry.Text);
                    dataLabel.Text = a;
                }

            });
        }

        void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {

                FingerprintAuthenticationResult authResult = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Heads up!", "I would like to use your biometrics, please!"));

                if (authResult.Authenticated)
                {
                    //Models.BioProfileEntity dataInfo = await BiometricsService.GetPassword();

                    // if (dataInfo != null)

                    string a = CryptoManagerApp.DecryptData(dataLabel.Text);
                    dataLabel.Text = a;
                     //   = JsonConvert.SerializeObject(dataInfo);
                }           

            });
        }
    }
}
