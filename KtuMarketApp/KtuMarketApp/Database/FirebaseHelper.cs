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


        /*----------------------------- Ürün İşlemleri ---------------------------------------*/

        // Ürün fotoğrafı al
        public async Task<string> GetProductPhotoUrl(string imagename, FileResult result)
        {
            string imageurlstring = await new FirebaseStorage("xamarinfirebase-3a73b.appspot.com").Child("ProductPhotos").Child(imagename).PutAsync(await result.OpenReadAsync());
            return imageurlstring;
        }

        // Ürün Ekle
        public async Task AddProduct(string productbarcode, string productname, string productimageurl, string personname, string marketname, double productprice)
        {
            await firebase.Child("Products").PostAsync(new Product()
            {
                ProductBarcode = productbarcode,
                ProductName = productname,
                ProductImageUrl = productimageurl,
                PriceAddedDate = DateTime.Now,
                PersonName = personname,
                MarketName = marketname,
                ProductPrice = productprice
            });
        }

        // Filtreli ürünleri Listele
        public async Task<List<Product>> GetAllProduct(string productname)
        {
            return (await firebase.Child("Products").OnceAsync<Product>()).Select(item => new Product()
            {
                ProductBarcode = item.Object.ProductBarcode,
                ProductName = item.Object.ProductName,
                ProductImageUrl = item.Object.ProductImageUrl,
                PriceAddedDate = item.Object.PriceAddedDate,
                PersonName = item.Object.PersonName,
                MarketName = item.Object.MarketName,
                ProductPrice = item.Object.ProductPrice
            }).Where(a => a.ProductName.StartsWith(productname.ToUpper())).OrderByDescending(product => product.PriceAddedDate).OrderByDescending(product => product.ProductPrice).ToList();

        }

        // Sorgulanan barkodu alan bir ürün var mı?
        public async Task<Product> GetAllProductWithBarcode(string barcodestring)
        {
            return (await firebase.Child("Products").OnceAsync<Product>()).Select(item => new Product()
            {
                ProductBarcode = item.Object.ProductBarcode,
                ProductName = item.Object.ProductName,
                ProductImageUrl = item.Object.ProductImageUrl,
                PriceAddedDate = item.Object.PriceAddedDate,
                PersonName = item.Object.PersonName,
                MarketName = item.Object.MarketName,
                ProductPrice = item.Object.ProductPrice
            }).Where(a => a.ProductBarcode == barcodestring).FirstOrDefault(); ;
        }

        // Kullanıcının Ürününü Takip Listesine Ekler
        public async Task AddFavouriteProduct(Product product, string username)
        {
            await firebase.Child("Favourites").PostAsync(new Product()
            {
                ProductBarcode = product.ProductBarcode,
                ProductName = product.ProductName,
                ProductImageUrl = product.ProductImageUrl,
                PriceAddedDate = product.PriceAddedDate,
                PersonName = username,
                MarketName = product.MarketName,
                ProductPrice = product.ProductPrice
            });
        }

        // Favori Listesinden Ürün Kaldırma
        public async Task DeleteFavouriteProduct(string personname, string productname, string priceaddeddate)
        {
            var DeleteFavouriteProduct = (await firebase.Child("Favourites").OnceAsync<Product>()).Where(p =>
            p.Object.PersonName == personname && p.Object.ProductName == productname && p.Object.PriceAddedDate.ToString() == priceaddeddate).FirstOrDefault();

            await firebase.Child("Favourites").Child(DeleteFavouriteProduct.Key).DeleteAsync();
        }

        /*----------------------------------- Kullanıcı İşlemleri -----------------------------------------*/

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

        // Kullanıcının Takip Listesine Aldığı Ürünleri Listeler
        public async Task<List<Product>> GetAllFavouriteProduct(string personname)
        {
            return (await firebase.Child("Favourites").OnceAsync<Product>()).Select(item => new Product()
            {
                ProductBarcode = item.Object.ProductBarcode,
                ProductName = item.Object.ProductName,
                ProductImageUrl = item.Object.ProductImageUrl,
                PriceAddedDate = item.Object.PriceAddedDate,
                PersonName = item.Object.PersonName,
                MarketName = item.Object.MarketName,
                ProductPrice = item.Object.ProductPrice
            }).Where(a => a.PersonName == personname).ToList();
        }

    }
}
