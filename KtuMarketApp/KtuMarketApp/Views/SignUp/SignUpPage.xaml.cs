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
        private string ImageUrlString { get; set; }
        public SignUpPage()
        {
            InitializeComponent();

        }

        private async void GetPictureFromSource(object sender, EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Lütfen Bir Fotoğraf Seçiniz!"
            });

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

                resultImage.Source = ImageSource.FromStream(() => stream);
                ImageUrlString = await firebaseHelper.GetPhotoUrl(result.FileName, result);
                photopathtext.Text = result.FileName;
            }
        }

        private async void TakePictureFromCamera(object sender, EventArgs e)
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

                resultImage.Source = ImageSource.FromStream(() => stream);
                ImageUrlString = await firebaseHelper.GetPhotoUrl(result.FileName, result);
                photopathtext.Text = result.FileName;
            }
        }

        async void SignUpActionClicked(object sender, EventArgs e)
        {
            var user = await firebaseHelper.GetPerson(username.Text, password.Text);

            if (!(string.IsNullOrEmpty(username.Text) && string.IsNullOrEmpty(username.Text)))
            {
                if (user != null)
                {
                    await DisplayAlert("Kullanıcı Mevcut", "Başka hesap ismi ile kayıt olmayı deneyin", "Tamam");
                }
                else
                {
                    // Kullanıcı bilgilerini kaydet
                    await firebaseHelper.AddPerson(username.Text, password.Text, ImageUrlString);
                    await DisplayAlert("Üye Kaydınız Tamamlandı", "Anasayfaya yönlendiriliyorsunuz...", "Tamam");
                    await Navigation.PushAsync(new MainPage());
                }
            }
            else
            {
                await DisplayAlert("Hatalı Giriş", "Verilen Alanları Eksiksiz Doldurunuz", "Tamam");
            }
            
        }
    }
}