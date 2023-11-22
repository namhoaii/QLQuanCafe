using QLQuanCafe.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCafe.Data
{
    public class SanPhamDatabase
    {
        readonly SQLiteAsyncConnection database;

        public SanPhamDatabase(SQLiteAsyncConnection connection)
        {
            this.database = connection;
            database.CreateTableAsync<SanPham>().Wait();
        }

        public Task<List<SanPham>> GetSanPhamAsync()
        {
            //Get all 
            return database.Table<SanPham>().ToListAsync();
        }

        public Task<SanPham> GetSanPhamAsync(int id)
        {
            // Get a specific
            return database.Table<SanPham>()
                            .Where(i => i.IDSanPham == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveSanPhamAsync(SanPham SanPham)
        {
            if (SanPham.IDSanPham != 0)
            {
                // Update an existing 
                return database.UpdateAsync(SanPham);
            }
            else
            {
                // Save a new 
                return database.InsertAsync(SanPham);
            }
        }

        public Task<int> DeleteSanPhamAsync(SanPham SanPham)
        {
            // Delete 
            return database.DeleteAsync(SanPham);
        }
    }
}
