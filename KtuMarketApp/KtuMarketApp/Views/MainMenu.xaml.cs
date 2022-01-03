using KtuMarketApp.Models;
using KtuMarketApp.Views.Favourites;
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
                IconImageSource = "https://upload.wikimedia.org/wikipedia/commons/7/70/User_icon_BLACK-01.png",
            });
            this.Children.Add(new AddProduct(person)
            {
                IconImageSource = "https://cdn-icons-png.flaticon.com/512/2891/2891421.png"
            });
            this.Children.Add(new SearchProduct(person)
            {
                IconImageSource = "https://cdn-icons-png.flaticon.com/512/54/54481.png"
            });
            this.Children.Add(new FavouritesProduct(person.PersonName)
            {
                IconImageSource = "https://img.icons8.com/ios/2x/star.png"
            });
        }
    }
}