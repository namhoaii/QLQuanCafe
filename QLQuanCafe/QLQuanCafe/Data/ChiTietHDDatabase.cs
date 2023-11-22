using QLQuanCafe.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCafe.Data
{
    public class ChiTietHDDatabase
    {
        readonly SQLiteAsyncConnection database;

        public ChiTietHDDatabase(SQLiteAsyncConnection connection)
        {
            this.database = connection;
            database.CreateTableAsync<ChiTietHD>().Wait();
        }

        public Task<List<ChiTietHD>> GetChiTietHDAsync()
        {
            //Get all 
            return database.Table<ChiTietHD>().ToListAsync();
        }

        public Task<ChiTietHD> GetChiTietHDAsync(int id)
        {
            // Get a specific
            return database.Table<ChiTietHD>()
                            .Where(i => i.IDChiTietHD == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveChiTietHDAsync(ChiTietHD ChiTietHD)
        {
            if (ChiTietHD.IDChiTietHD != 0)
            {
                // Update an existing 
                return database.UpdateAsync(ChiTietHD);
            }
            else
            {
                // Save a new 
                return database.InsertAsync(ChiTietHD);
            }
        }

        public Task<int> DeleteChiTietHDAsync(ChiTietHD ChiTietHD)
        {
            // Delete 
            return database.DeleteAsync(ChiTietHD);
        }
    }
}
