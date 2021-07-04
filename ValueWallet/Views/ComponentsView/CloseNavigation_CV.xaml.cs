using System;
using ValueWallet.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ValueWallet.Views.ComponentsView
{
    public partial class CloseNavigation_CV : ContentView
    {
        public event EventHandler Clicked;

        public CloseNavigation_CV()
        {
            InitializeComponent();

            if (LocalDeviceInfo.CurrentDevice.IsNotchIosDevice)
                NotchBx.IsVisible = true;
            else
                NotchBx.IsVisible = false;

            BindingContext = this;
        }

        void Close_Tapped(Object sender, EventArgs e)
        {
            if(Clicked != null)
            {
                Clicked(this, new EventArgs());
            }
            else
            {
                if (Navigation.ModalStack.Count > 0)
                {
                    Navigation.PopModalAsync();
                }
                else if (Navigation.NavigationStack.Count > 0)
                {
                    Navigation.PopAsync();
                }
            }
        }
    }
}
