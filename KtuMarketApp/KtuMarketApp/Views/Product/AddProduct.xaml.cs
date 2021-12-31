using KtuMarketApp.Database;
using KtuMarketApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KtuMarketApp.Views.Product
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProduct : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        private string ImageUrlString { get; set; }
        private Person _person;
        public AddProduct(Person person)
        {
            InitializeComponent();
            _person = person;
        }

        // Fotoğraf Alan Fonksiyon
        private async void TakePhoto_Clicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

                resultImage.Source = ImageSource.FromStream(() => stream);
                ImageUrlString = await firebaseHelper.GetProductPhotoUrl(result.FileName, result);
                photopathtext.Text = result.FileName;
            }
        }

        // Ürün Ekle
        private async void AddProduct_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.AddProduct(urunbarcode.Text, urunadi.Text.ToUpper(), ImageUrlString, _person.PersonName, market.SelectedItem.ToString(), Convert.ToDouble(urunfiyati.Text));
            await DisplayAlert("Ürün Eklendi", $"{urunadi.Text.ToUpper()} {market.SelectedItem} adlı markete eklendi", "Tamam");
            urunadi.Text = string.Empty;
            urunbarcode.Text = string.Empty;
            urunfiyati.Text = string.Empty;
        }
    }
}