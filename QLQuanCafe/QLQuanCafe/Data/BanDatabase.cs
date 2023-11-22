using QLQuanCafe.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCafe.Data
{
    public class BanDatabase
    {
        readonly SQLiteAsyncConnection database;

        public BanDatabase(SQLiteAsyncConnection connection)
        {
            this.database = connection;
            database.CreateTableAsync<Ban>().Wait();
        }

        public Task<List<Ban>> GetBanAsync()
        {
            //Get all 
            return database.Table<Ban>().ToListAsync();
        }

        public Task<Ban> GetBanAsync(int id)
        {
            // Get a specific
            return database.Table<Ban>()
                            .Where(i => i.IDBan == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveBanAsync(Ban Ban)
        {
            if (Ban.IDBan != 0)
            {
                // Update an existing 
                return database.UpdateAsync(Ban);
            }
            else
            {
                // Save a new 
                return database.InsertAsync(Ban);
            }
        }

        public Task<int> DeleteBanAsync(Ban Ban)
        {
            // Delete 
            return database.DeleteAsync(Ban);
        }
    }
}
