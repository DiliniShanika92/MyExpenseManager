using Firebase.Database;
using Firebase.Database.Query;
using my_expense_manager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace my_expense_manager.Services
{
    class FirebaseServices
    {
        FirebaseClient Client;
        public FirebaseServices()
        {
            Client = new FirebaseClient("https://myexpensesmanager-591f4-default-rtdb.asia-southeast1.firebasedatabase.app/");
        }

        public async Task<bool> CreateTrans(transaction trans)
        {

            try
            {
                _ = await Client.Child("Transaction").PostAsync(JsonConvert.SerializeObject(trans));

                return true;
            }
            catch (Exception e)
            {


                return false;
            }
        }
        public async Task<bool> CreateTransList(List<transaction> transactions)
        {
            try
            {
                foreach (var trans in transactions)
                {
                    var a = (await Client.Child("Transaction").OnceAsync<transaction>()).Where(u => u.Object.Id == trans.Id).FirstOrDefault();


                    if (a != null)
                    {
                        await Client.Child("Transaction").Child(a.Key).PutAsync(JsonConvert.SerializeObject(trans));
                    }
                    else
                    {
                        _ = await Client.Child("Transaction").PostAsync(JsonConvert.SerializeObject(trans));

                    }

                }

                return true;
            }
            catch (Exception e)
            {


                return false;
            }
        }
        public async Task<List<transaction>> AllTransaction()
        {
            try
            {

                var a = (await Client.Child("Transaction").OnceAsync<transaction>()).Select(item => new transaction
                {
                    Id = item.Object.Id,
                    Amount = item.Object.Amount,
                    DateAndTime = item.Object.DateAndTime,
                    Discription = item.Object.Discription,
                    Category = item.Object.Category,
                    TransactionType = item.Object.TransactionType,


                }).ToList();

                return a;


            }
            catch (Exception e)
            {


                return null;
            }
        }

        public async Task<transaction> GetById(int id)
        {
            try
            {

                transaction tr = (await Client.Child("Transaction").OnceAsync<transaction>()).Where(u => u.Object.Id == id).Select(item => new transaction
                {
                    Id = item.Object.Id,
                    Amount = item.Object.Amount,
                    DateAndTime = item.Object.DateAndTime,
                    Discription = item.Object.Discription,
                    Category = item.Object.Category,
                    TransactionType = item.Object.TransactionType,

                }).First();

                return tr;
            }
            catch (Exception e)
            {
                return null;

            }
        }
        public async Task<bool> UpdateTrans(int id, transaction tr)
        {
            try
            {
                var a = (await Client.Child("Transaction").OnceAsync<transaction>()).Where(u => u.Object.Id == id).FirstOrDefault();

                await Client.Child("Transaction").Child(a.Key).PutAsync(JsonConvert.SerializeObject(tr));

                return true;
            }
            catch (Exception e)
            {


                return false;
            }
        }

        public async Task<bool> DeleteTrans(int id)
        {
            try
            {
                var a = (await Client.Child("Transaction").OnceAsync<transaction>()).Where(u => u.Object.Id == id).FirstOrDefault();
                await Client.Child("Transaction").Child(a.Key).DeleteAsync();

                return true;
            }
            catch (Exception e)
            {


                return false;
            }
        }
        public async Task<bool> DeleteTransAll()
        {
            try
            {
                await Client.Child("Transaction").DeleteAsync();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
    }
}
