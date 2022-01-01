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


        public ProductDetail(Models.Product product)
        {
            InitializeComponent();
            _product = product;
            BindingContext = _product;

        }

        // Kullanıcının Takip Etmek İstediği Ürün Listesine Ekler
        private async void AddFavourite_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Models.Product product = btn.CommandParameter as Models.Product;

            await firebaseHelper.AddFavouriteProduct(product);

            await DisplayAlert("Ürün Takip Listesine Eklendi", $"{product.ProductName} ürünü takip listesine eklendi.", "Tamam");

        }
    }
}