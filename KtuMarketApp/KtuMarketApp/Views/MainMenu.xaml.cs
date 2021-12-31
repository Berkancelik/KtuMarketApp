using KtuMarketApp.Models;
using KtuMarketApp.Views.Product;
using KtuMarketApp.Views.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KtuMarketApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : TabbedPage
    {
        public MainMenu(Person person)
        {
            InitializeComponent();
            BindingContext = person;

            this.Children.Add(new UserProfile(person)
            {
                IconImageSource = "https://cdn-icons.flaticon.com/png/512/1144/premium/1144760.png?token=exp=1640894715~hmac=ee2a03636d8e8b56f9ac60dd6ca637c2",
            });
            this.Children.Add(new AddProduct(person)
            {
                IconImageSource = "https://cdn-icons-png.flaticon.com/512/2891/2891421.png"
            });
            this.Children.Add(new SearchProduct(person)
            {
                IconImageSource = "https://cdn-icons-png.flaticon.com/512/54/54481.png"
            });
        }
    }
}