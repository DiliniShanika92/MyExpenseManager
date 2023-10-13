using my_expense_manager.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace my_expense_manager.Services
{

    public class SqHelperService
    {
        private readonly SQLiteAsyncConnection db;

        public SqHelperService(string dbPath)
        {

            db = new SQLiteAsyncConnection(dbPath);
            
            db.CreateTableAsync<transaction>();
            db.CreateTableAsync<RecycleTransaction>();
            db.CreateTableAsync<Category>();
           
        }


        
        public async Task<int> Create(transaction trs)
        {
            try
            {
                return await db.InsertAsync(trs);
            }
            catch (Exception e)
            {
                _ = Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                return 0;
            }

        }
        public async Task<int> CreateRecycle(RecycleTransaction trs)
        {
            try
            {
                return await db.InsertAsync(trs);
            }
            catch (Exception e)
            {
                _ = Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                return 0;
            }
        }
        public async Task<int> CreateCategory(Category trs)
        {
            try
            {
                return await db.InsertAsync(trs);
            }
            catch (Exception e)
            {
                _ = Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                return 0;
            }

        }
        public async Task<IEnumerable<RecycleTransaction>> GetAllRecycle()
        {

            try
            {
                var a = await db.Table<RecycleTransaction>().ToListAsync();
                return a;
            }
            catch (Exception w)
            {

                throw;
            }
        }
        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            try
            {
                var a = await db.Table<Category>().ToListAsync();
                return a;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<int> DeleteRecycle(int trs)
        {
            try
            {
                return await db.DeleteAsync<RecycleTransaction>(trs);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<int> DeleteCategory(int trs)
        {
            try
            {
                return await db.DeleteAsync<Category>(trs);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<int> DeleteRecycleAll()
        {
            try
            {
                return await db.DeleteAllAsync<RecycleTransaction>();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<int> Update(transaction trs)
        {
            try
            {
                return await db.UpdateAsync(trs);
            }
            catch (Exception e)
            {
                _ = Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                return 0;
            }
        }
        public Task<int> CreateAll(List<transaction> trs)
        {
            try
            {
                return db.InsertAllAsync(trs);
            }
            catch (Exception e)
            {
                _ = Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                return null;
            }
        }
        public async Task<IEnumerable<transaction>> GetAll()
        {
            return await db.Table<transaction>().ToListAsync();
        }
        public async Task<int> Delete(transaction trs)
        {
            return await db.DeleteAsync(trs);
        }
        public Task Clear()
        {
            return db.DeleteAllAsync<transaction>();

        }
    }
}
