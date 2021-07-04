using System;
using ValueWallet.Services.CoreService;
using ValueWallet.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ValueWallet
{
    public partial class App : Application
    {
        public App()
        {

            InitializeComponent();

            ControlStyleService.RegisterStyles();

            MainPage = new Views.RegisterNLogin.LoginNewUserPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
