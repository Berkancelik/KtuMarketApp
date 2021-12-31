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
        Models.Product _product = new Models.Product();
        public ProductDetail(Models.Product product)
        {
            InitializeComponent();
            _product = product;
            BindingContext = _product;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}