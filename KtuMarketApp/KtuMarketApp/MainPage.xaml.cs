using KtuMarketApp.Views.PromotionPages;
using KtuMarketApp.Views.SignUp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KtuMarketApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void GoToPromotions(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Promotions());
        }
        
        private async void GoToSignUp(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }


    }
}
