﻿using System;
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

            //MainPage = new OnBoardingPage();

            MainPage = new MyPage();
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
