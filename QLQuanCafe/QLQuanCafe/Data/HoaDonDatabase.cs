using QLQuanCafe.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCafe.Data
{
    public class HoaDonDatabase
    {
        readonly SQLiteAsyncConnection database;

        public HoaDonDatabase(SQLiteAsyncConnection connection)
        {
            this.database = connection;
            database.CreateTableAsync<HoaDon>().Wait();
        }

        public Task<List<HoaDon>> GetHoaDonAsync()
        {
            //Get all 
            return database.Table<HoaDon>().ToListAsync();
        }

        public Task<HoaDon> GetHoaDonAsync(int id)
        {
            // Get a specific
            return database.Table<HoaDon>()
                            .Where(i => i.IDHoaDon == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveHoaDonAsync(HoaDon HoaDon)
        {
            if (HoaDon.IDHoaDon != 0)
            {
                // Update an existing 
                return database.UpdateAsync(HoaDon);
            }
            else
            {
                // Save a new 
                return database.InsertAsync(HoaDon);
            }
        }

        public Task<int> DeleteHoaDonAsync(HoaDon HoaDon)
        {
            // Delete 
            return database.DeleteAsync(HoaDon);
        }
    }
}
