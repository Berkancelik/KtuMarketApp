using KtuMarketApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KtuMarketApp.Views.SignUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUpPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public SignUpPage()
        {
            InitializeComponent();

        }

        async void GetPictureFromSource(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Lütfen Bir Fotoğraf Seçiniz!"
            });

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

                resultImage.Source = ImageSource.FromStream(() => stream);
                photopathtext.Text = result.FullPath;
            }
        }

        async void TakePictureFromCamera(object sender, EventArgs e)
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

                resultImage.Source = ImageSource.FromStream(() => stream);
                photopathtext.Text = result.FullPath;
            }
        }

        async void SignUpActionClicked(object sender, EventArgs e)
        {
            await firebaseHelper.AddPerson(username.Text, password.Text, "url");
        }
    }
}