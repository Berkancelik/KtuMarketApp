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
    public partial class SearchProduct : ContentPage
    {
        public SearchProduct()
        {
            InitializeComponent();

            listview.ItemsSource = new string[]
            {
                "item 1",
                "item 2",
                "item 3"
            };

        }
    }
}