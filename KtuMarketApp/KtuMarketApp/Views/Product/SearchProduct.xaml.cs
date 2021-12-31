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

        public SearchProduct()
        {
            InitializeComponent();

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            listview.ItemsSource = await firebaseHelper.GetAllProduct();
        }
    }
}