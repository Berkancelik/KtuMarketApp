using KtuMarketApp.Models;
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

            this.Children.Add(new UserProfile(person) {
                Title="Kullanıcı Profili", 
                IconImageSource= "https://cdn-icons.flaticon.com/png/512/552/premium/552909.png?token=exp=1640818254~hmac=12bfc4860085754b1065cc6dacc3e0da" });
            

        }
    }
}