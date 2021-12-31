using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KtuMarketApp.Database;
using KtuMarketApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KtuMarketApp.Views.Product
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchProduct : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        private string ImageUrlString { get; set; }
        private Person _person;

        public SearchProduct(Person person)
        {
            InitializeComponent();
            _person = person;

        }


        private async void listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Models.Product selectedItem = e.Item as Models.Product;
            await Navigation.PushAsync(new ProductDetail(selectedItem));
        }


        private async void SearchButton_Clicked(object sender, EventArgs e)
        {
            // Ürün Adı Entry'si boş değilse
            if (!string.IsNullOrEmpty(urunadi.Text))
            {
                if (urunadi.Text.Length >= 3)
                {
                    // Aranan Ürünü Sorgula
                    var result = await firebaseHelper.GetAllProduct(urunadi.Text);
                    if (!result.Count.Equals(0))
                    {
                        // Aranan Ürünü Listele
                        listview.ItemsSource = result;
                    }
                    else
                    {
                        await DisplayAlert("Ürün Bulunamadı", "Ürün Ekleme Sayfasına Yönlendiriliyorsunuz.", "Tamam");
                        await Navigation.PushAsync(new AddProduct(_person));
                    }

                    
                }
                else
                {
                    await DisplayAlert("Eksik Karakter", "Ürün için en az üç karakter giriniz!", "Tamam");
                }
            }
            else
            {
                await DisplayAlert("Geçersiz Ürün Adı", "Ürün için en az üç karakter giriniz!", "Tamam");
            }


        }
    }
}