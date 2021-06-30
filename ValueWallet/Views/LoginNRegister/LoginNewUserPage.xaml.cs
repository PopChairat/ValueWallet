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

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            bool isPass = await BiometricsService.IsPassAuthUser();

            if (isPass)
            {
                BiometricsService.SetUserLogin(userEntry.Text, passEntry.Text, Domain.Entities.LoginBy.Biometric);
            }
        }

        async void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            bool isPass = await BiometricsService.IsPassAuthUser();

            if (isPass)
            {
                Models.BioProfileEntity userDto = await BiometricsService.GetUserLogin();
                dataLabel.Text = JsonConvert.SerializeObject(userDto);
            }
        }
    }
}
