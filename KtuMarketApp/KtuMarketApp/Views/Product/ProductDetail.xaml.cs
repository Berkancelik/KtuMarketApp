using KtuMarketApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KtuMarketApp.Views.Product
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetail : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        Models.Product _product = new Models.Product();

        private string _username;

        public ProductDetail(Models.Product product, string username)
        {
            InitializeComponent();
            _product = product;
            BindingContext = _product;
            _username = username;
        }

        // Kullanıcının Takip Etmek İstediği Ürün Listesine Ekler
        private async void AddFavourite_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Models.Product product = btn.CommandParameter as Models.Product;

            await firebaseHelper.AddFavouriteProduct(product, _username);

            await DisplayAlert("Ürün Takip Listesine Eklendi", $"{product.ProductName} ürünü takip listesine eklendi.", "Tamam");

        }
    }
}