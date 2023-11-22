using QLQuanCafe.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCafe.Data
{
    public class NhanVienDatabase
    {
        readonly SQLiteAsyncConnection database;

        public NhanVienDatabase(SQLiteAsyncConnection connection)
        {
            this.database = connection;
            database.CreateTableAsync<NhanVien>().Wait();
        }

        public Task<List<NhanVien>> GetNhanVienAsync()
        {
            //Get all 
            return database.Table<NhanVien>().ToListAsync();
        }

        public Task<int> SaveNhanVienAsync(NhanVien NhanVien)
        {
            return database.InsertAsync(NhanVien);
        }
        public Task<int> UpdateNhanVienAsync(NhanVien NhanVien)
        {
            return database.UpdateAsync(NhanVien);
        }

        public Task<int> DeleteNhanVienAsync(NhanVien NhanVien)
        {
            // Delete 
            return database.DeleteAsync(NhanVien);
        }
    }
}
