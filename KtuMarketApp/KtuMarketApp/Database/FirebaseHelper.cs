using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using KtuMarketApp.Models;
using Xamarin.Essentials;

namespace KtuMarketApp.Database
{
    public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://xamarinfirebase-3a73b-default-rtdb.europe-west1.firebasedatabase.app/");
        public FirebaseHelper()
        {

        }




        // Ürün fotoğrafı al
        public async Task<string> GetProductPhotoUrl(string imagename, FileResult result)
        {
            string imageurlstring = await new FirebaseStorage("xamarinfirebase-3a73b.appspot.com").Child("ProductPhotos").Child(imagename).PutAsync(await result.OpenReadAsync());
            return imageurlstring;
        }

        // Ürün Ekle
        public async Task AddProduct(string productname, string productimageurl, string personname, string marketname, double productprice)
        {
            await firebase.Child("Products").PostAsync(new Product()
            {
                ProductName = productname,
                ProductImageUrl = productimageurl,
                PriceAddedDate = DateTime.Now,
                PersonName = personname,
                MarketName = marketname,
                ProductPrice = productprice
            });
        }

        public async Task<List<Product>> GetAllProduct()
        {
            return (await firebase.Child("Products").OnceAsync<Product>()).Select(item => new Product()
            {
                ProductName = item.Object.ProductName,
                ProductImageUrl = item.Object.ProductImageUrl,
                PriceAddedDate = item.Object.PriceAddedDate,
                PersonName = item.Object.PersonName,
                MarketName = item.Object.MarketName,
                ProductPrice = item.Object.ProductPrice
            }).ToList();
        }

        // Kullanıcı Kaydı Oluşturur
        public async Task AddPerson(string personname, string password, string userphotourl)
        {
            await firebase.Child("Persons").PostAsync(new Person()
            {
                PersonName = personname,
                Password = password,
                UserPhotoUrl = userphotourl
            });
        }

        // Kullanıcı Fotoğrafı Al
        public async Task<string> GetPhotoUrl(string imagename, FileResult result)
        {
            string imageurlstring = await new FirebaseStorage("xamarinfirebase-3a73b.appspot.com").Child("UserPhotos").Child(imagename).PutAsync(await result.OpenReadAsync());
            return imageurlstring;
        }

        // Sistemde bütün kayıtlı kullanıcıları çekiyor
        public async Task<List<Person>> GetAllPerson()
        {
            return (await firebase.Child("Persons").OnceAsync<Person>()).Select(item => new Person
            {
                PersonName = item.Object.PersonName,
                Password = item.Object.Password,
                UserPhotoUrl = item.Object.UserPhotoUrl
            }).ToList();
        }

        // Login İşleminde Kullanılıyor
        public async Task<Person> GetPerson(string personname, string password)
        {
            var allPersons = await GetAllPerson();
            await firebase.Child("Persons").OnceAsync<Person>();
            return allPersons.Where(a => a.PersonName == personname && a.Password == password).FirstOrDefault();
        }

        // Kullanıcı Kayıt Silme İşlemi
        public async Task DeletePerson(string personname)
        {
            // Silinecek Hsabı Bul
            var toDeletePerson = (await firebase.Child("Persons").OnceAsync<Person>()).Where(a => a.Object.PersonName == personname).FirstOrDefault();

            // Hesabı Sil
            await firebase.Child("Persons").Child(toDeletePerson.Key).DeleteAsync();
        }

        // Şifre Güncelleme
        public async Task UpdatePerson(string personname, string newpassword)
        {
            var toUpdatePerson = (await firebase.Child("Persons").OnceAsync<Person>()).Where(a => a.Object.PersonName == personname).FirstOrDefault();

            await firebase.Child("Persons").Child(toUpdatePerson.Key).PutAsync(new Person()
            {
                PersonName = toUpdatePerson.Object.PersonName,
                Password = newpassword,
                UserPhotoUrl = toUpdatePerson.Object.UserPhotoUrl
            });
        }

    }
}
