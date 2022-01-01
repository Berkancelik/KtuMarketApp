using KtuMarketApp.Database;
using KtuMarketApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KtuMarketApp.Views.Favourites
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavouritesProduct : ContentPage
    {

        FirebaseHelper firebaseHelper = new FirebaseHelper();
        private string _personname;

        public FavouritesProduct(string personname)
        {
            InitializeComponent();
            _personname = personname;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await firebaseHelper.GetAllFavouriteProduct(_personname);
        }

        private async void ListView_Refreshing(object sender, EventArgs e)
        {
            listView.ItemsSource = await firebaseHelper.GetAllFavouriteProduct(_personname);
            listView.IsRefreshing = false;
        }

        private async void RemoveFavouriteItem(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;

            var product = menuItem.CommandParameter as Models.Product;

            await firebaseHelper.DeleteFavouriteProduct(product.PersonName, product.ProductName, product.PriceAddedDate.ToString());
            
            listView.ItemsSource = await firebaseHelper.GetAllFavouriteProduct(_personname);

        }
    }
}