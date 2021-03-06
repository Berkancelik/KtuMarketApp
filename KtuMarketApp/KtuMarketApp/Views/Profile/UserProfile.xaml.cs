using KtuMarketApp.Database;
using KtuMarketApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KtuMarketApp.Views.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfile : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        private string _person;
        public UserProfile(Person person)
        {
            InitializeComponent();
            UserProfileContainer.BindingContext = person;
            _person = person.PersonName;
        }
       

        private async void DeletePerson(object sender, EventArgs e)
        {
            await firebaseHelper.DeletePerson(_person);
            await DisplayAlert("Hesap Silindi", "Anasayfaya Yönlendiriliyorsunuz...", "Tamam");
            await Navigation.PopToRootAsync();
        }

        private async void UpdatePerson(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(NewPasswordEntry.Text))
            {
                await firebaseHelper.UpdatePerson(_person, NewPasswordEntry.Text);
                await DisplayAlert("Şifreniz Güncellendi", $"Yeni şifreniz : {NewPasswordEntry.Text}", "Tamam");
                NewPasswordEntry.Text = string.Empty;
                

            }
            else
            {
                await DisplayAlert("Geçersiz Değer", "Verilen Alanı Eksiksiz Doldurunuz!", "Tamam");
            }


        }

    }
}