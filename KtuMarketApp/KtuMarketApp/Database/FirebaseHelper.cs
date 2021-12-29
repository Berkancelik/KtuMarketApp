using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using KtuMarketApp.Models;

namespace KtuMarketApp.Database
{
    public class FirebaseHelper
    {
        FirebaseClient firebase = new FirebaseClient("https://xamarinfirebase-3a73b-default-rtdb.europe-west1.firebasedatabase.app/");
        public FirebaseHelper()
        {

        }

        // Insert Data
        public async Task AddProduct(string productname, string productimageurl, string marketname, double productprice)
        {
            await firebase.Child("Products").PostAsync(new Product()
            {
                ProductName = productname,
                ProductImageUrl = productimageurl,
                PriceDate = DateTime.Now,
                MarketName = marketname,
                ProductPrice = productprice
            });
        }

        public async Task AddPerson(string personname, string password, string userphotourl)
        {
            await firebase.Child("Persons").PostAsync(new Person()
            {
                PersonName = personname,
                Password = password,
                UserPhotoUrl = userphotourl
            });
        }
    }
}
