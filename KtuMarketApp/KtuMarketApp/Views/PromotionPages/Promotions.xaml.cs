using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KtuMarketApp.Views.PromotionPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Promotions : CarouselPage
    {
        public Promotions()
        {
            InitializeComponent();
        }
    }
}