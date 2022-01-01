using KtuMarketApp.Database;
using KtuMarketApp.Views;
using KtuMarketApp.Views.Profile;
using KtuMarketApp.Views.PromotionPages;
using KtuMarketApp.Views.SignUp;
using System;
using Xamarin.Forms;

namespace KtuMarketApp
{
    public partial class MainPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public MainPage()
        {
            InitializeComponent();
        }

        private async void GoToPromotions(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Promotions());
        }

        private async void SignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }

        private async void SignInClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameEntry.Text) && string.IsNullOrEmpty(PasswordEntry.Text))
            {
                await DisplayAlert("Hatalı Veri", "Verilen Alanları Doldurunuz!", "Tamam");
            }
            else
            {
                // Arayüzden alınan kullanıcı bilgileri karşılaştırılıyor.
                var person = await firebaseHelper.GetPerson(NameEntry.Text, PasswordEntry.Text);

                if (person != null)
                {
                    await DisplayAlert($"Kullanıcı Bilgileri Doğru", "Ana Menüye yönlendiriliyorsunuz...", "Tamam");
                    await Navigation.PushAsync(new MainMenu(person));
                }
                else
                {
                    await DisplayAlert("Kullanıcı Bilgileri Hatalı", "Ana Sayfaya Yönlendiriliyorsunuz...", "Tamam");
                }
            }





        }
    }
}
